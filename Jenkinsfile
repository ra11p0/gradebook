import groovy.json.JsonSlurperClassic
import groovy.json.*

/*
    params:{
        dbHost: ip;
        dbPort: number;
        dbName: string;
        dbUid: string;
        dbPassword: password;

        applicationUrl: url;
        apiPort: number;
        targetUrl: url;
        jwtSecret: password;
        smtpHost: string;
        smtpPort: number;
        smtpUsername: string;
        smtpPassword: password;
        smtpDefaultSender: email;
        defaultSenderName: string;
        nodeApiUrl: url;
        environment: string;
        gitBranchPattern: regex;
    }
*/
void setBuildStatus(String message, String state) {
  step([
      $class: "GitHubCommitStatusSetter",
      reposSource: [$class: "ManuallyEnteredRepositorySource", url: "https://github.com/ra11p0/gradebook"],
      contextSource: [$class: "ManuallyEnteredCommitContextSource", context: "ci/jenkins/build-status"],
      errorHandlers: [[$class: "ChangingBuildStatusErrorHandler", result: "UNSTABLE"]],
      statusResultSource: [ $class: "ConditionalStatusResultSource", results: [[$class: "AnyBuildResult", message: message, state: state]] ]
  ]);
}

def jsonSlurpLax(String jsonText){
    return new JsonSlurperClassic().parseText(
        new JsonBuilder(
            new JsonSlurper()
                .setType(JsonParserType.LAX)
                .parseText(jsonText)
        )
        .toString()
    )
}

def prepareAppSettings() {
    def appsettingsTemplateText = readFile env.WORKSPACE+"/ci/appsettings.template.json"

    println appsettingsTemplateText

    appSettings = jsonSlurpLax(appsettingsTemplateText)

    appSettings.ConnectionStrings.DefaultAppDatabase = "server="+params.dbHost+";port="+params.dbPort+";database="+params.dbName+";uid="+params.dbUid+";password="+params.dbPassword.plainText+";AllowUserVariables=True;"
    appSettings.Urls = params.applicationUrl + ":" + params.apiPort
    appSettings.JWT.ValidAudience = params.targetUrl
    appSettings.JWT.ValidIssuer = params.targetUrl
    appSettings.JWT.Secret = params.jwtSecret.encryptedValue

    //  smtp configuration
    appSettings.smtp.host = params.smtpHost
    appSettings.smtp.port = params.smtpPort
    appSettings.smtp.username = params.smtpUsername
    appSettings.smtp.password = params.smtpPassword.plainText
    appSettings.smtp.defaultSender = params.smtpDefaultSender
    appSettings.smtp.defaultSenderName = params.defaultSenderName
    appSettings.TargetUrl = params.targetUrl

    def jsonPrepared = new JsonBuilder(appSettings).toPrettyString()

    println jsonPrepared
    writeFile(file:'ci/appsettings.Production.json', text: jsonPrepared)
    
    def envFileText = readFile env.WORKSPACE + '/ci/.env.template'
    
    envFileText = envFileText.replace('{apiUrl}', params.nodeApiUrl)
    envFileText = envFileText.replace("{environment}", params.environment)
    envFileText = envFileText.replace("{port}", params.apiPort)
    if( params.environment == 'PRODUCTION') envFileText = envFileText.replace("{build}", '')
    else envFileText = envFileText.replace("{build}", env.BUILD_TAG)

    println envFileText

    writeFile(file:'ci/.env', text: envFileText)
}

def getGitBranchName() {
    return scm.branches[0].name
}

pipeline{        
    agent any;
    stages {
        stage('prepare') {
            steps {
                slackSend color: "good", message: "Job ${BUILD_TAG} started."
                script {
                    System.setProperty("org.jenkinsci.plugins.durabletask.BourneShellScript.HEARTBEAT_CHECK_INTERVAL", "86400");
                }
                checkout([$class: 'GitSCM', branches: [[name:  params.gitBranchPattern]], extensions: [], userRemoteConfigs: [[url: 'https://github.com/ra11p0/gradebook.git']]])
            }
        }  

        stage('prepare appsettings and .env') {
            steps {
                prepareAppSettings()
                
                sh 'cp -f ./ci/.env ./frontend/'
                sh 'cp -f ./ci/appsettings.Production.json ./backend/src/Api/appsettings.Production.json'
                sh 'cp -f ./ci/appsettings.Production.json ./backend/src/Gradebook.Foundation.Identity/appsettings.json'
                sh 'cp -f ./ci/appsettings.Production.json ./backend/src/Gradebook.Foundation.Database/appsettings.json'
                sh 'cp -f ./ci/appsettings.Production.json ./backend/src/Gradebook.Permissions.Database/appsettings.json'
                sh 'cp -f ./ci/appsettings.Production.json ./backend/src/Gradebook.Settings.Database/appsettings.json '
            }
        }
    
        stage('frontend install dependencies'){
            steps {
                sh 'cd frontend; npm cache clean -f; '
                sh 'cd frontend; export NODE_OPTIONS="--max-old-space-size=2048"; npm install -f --noproxy registry.npmjs.org --maxsockets 1;'
            }
        }
        
        stage('tests') {
            parallel{
                stage('backend unit tests') {
                    steps {
                        sh 'cd backend; dotnet test --filter TestCategory=Unit -l "console;verbosity=normal"'
                    }
                }
                stage('frontend test'){
                    steps {
                        sh 'cd frontend; npm run test;'
                    }
                }
            }
        }
        
        stage('build'){
            parallel{
                stage('build frontend'){
                    steps {
                        sh 'cd frontend; export NODE_OPTIONS="--max-old-space-size=2048"; npm run build;'
                    }
                }
                stage('build backend'){
                    steps {
                        sh 'cd backend; dotnet build -c Release'
                    }
                }
            }
        }

        stage('migrate databases'){
            steps {
                sh 'sudo systemctl stop kestrel-${JOB_NAME}'
                sh "sudo docker exec mysql /usr/bin/mysqldump -u ${params.dbUid} --password="+params.dbPassword.plainText+" ${params.dbName} > ${params.dbName}-${BUILD_TAG}-bak.sql --no-tablespaces"
                sh 'cd backend/src/Gradebook.Foundation.Identity; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Foundation.Database; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Permissions.Database; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Settings.Database; ~/.dotnet/tools/dotnet-ef database update;'
            }
            post{
                failure{
                    sh "cat ${params.dbName}-${BUILD_TAG}-bak.sql | sudo docker exec -i mysql /usr/bin/mysql -u ${params.dbUid} --password="+params.dbPassword.plainText+"  ${params.dbName}"
                    sh 'sudo systemctl start kestrel-${JOB_NAME}'
                }
            }
        }

        stage('deploy'){
            steps{
                sh 'rm -fr release; mkdir release;'
                sh 'cp -r frontend/build/ release/public'
                sh 'cp -r backend/src/Api/bin/Release/net6.0/ release/api'
                sh 'sudo systemctl start kestrel-${JOB_NAME}'
            }
        }
    }
     post{
            success{
                setBuildStatus("Build succeeded", "SUCCESS");
                slackSend color: "good", message: "Job ${BUILD_TAG} build and deployed successfully."
            }
            failure{
                setBuildStatus("Build failed", "FAILURE");
                slackSend color: "danger", message: "Job ${BUILD_TAG} failed."
            }
        }
}
    

