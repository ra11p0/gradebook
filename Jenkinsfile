//	database connection string (mysql)
//DEFAULT_DB_CONNECTION_STRING =""
//	api url
//APPLICATION_URL =""
//	api port
//API_PORT = ''
//	jwt secret
//JWT_SECRET= ""
//TARGET_URL = ''
//	public url to api (ex. http://{url}/api)
//NODE_API_URL = ""
//	environment (BETA, DEV, PROD)
//NODE_ENVIRONMENT = ''
//  smtp configuration
//	smtp hostname
//SMTP_HOST = ''
//	smtp port
//SMTP_PORT = ''
//	smtp username
//SMTP_USERNAME =''
//	smtp password
//SMTP_PASSWORD = ''
//	smtp default sender email (ex. no-reply@gradebook.ovh)
//SMTP_DEFAULT_SENDER = ''
//	smtp default friendly name (ex. Gradebook)
//SMTP_DEFAULT_SENDER_NAME = ''


import groovy.json.JsonSlurperClassic
import groovy.json.*


pipeline{        
    agent any;
    stages {
        stage('prepare') {
            steps {
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
                sh '''cp -f ./ci/appsettings.Production.json ./backend/src/Api/appsettings.Production.json'''
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
                        sh 'cd backend; dotnet test --filter TestCategory=Unit'
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
                sh 'cd backend/src/Gradebook.Foundation.Identity; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Foundation.Database; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Permissions.Database; ~/.dotnet/tools/dotnet-ef database update;'
                sh 'cd backend/src/Gradebook.Settings.Database; ~/.dotnet/tools/dotnet-ef database update;'
            }
        }
        stage('deploy'){
            steps{
                sh 'rm -fr release; mkdir release;'
                sh 'cp -r frontend/build/ release/public'
                sh '''cp -r backend/src/Api/bin/Release/net6.0/ release/api'''
                sh 'sudo systemctl start kestrel-${JOB_NAME}'
            }
        }
    }
}
    
