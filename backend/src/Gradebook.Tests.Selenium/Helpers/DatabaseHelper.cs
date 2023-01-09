using Gradebook.Foundation.Mailservice.MailMessages;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Dapper;
using Gradebook.Foundation.Common;

namespace Gradebook.Tests.Selenium.Helpers;

public static class DatabaseHelper
{
    public static string GetActivationLinkFromEmail(string email, DateTime? scanSince = null)
    {
        scanSince = scanSince ?? Time.UtcNow;
        var jsonString = ScanDatabase<string>(@"
            SELECT PayloadJson 
            FROM MailHistory 
            WHERE SendDateTime > @scanSince
                AND `To` LIKE @email", new { scanSince, email });
        var message = JsonConvert.DeserializeObject<ActivateAccountMailMessage>(jsonString);
        string url = ConfigurationManager.GetValue("Urls:ApplicationUrl");
        return $"{url}service/account/{message!.TargetGuid}/activation/{message!.AuthCode}";
    }

    public static string GetChangePasswordLinkFromEmail(string email, DateTime? scanSince = null)
    {
        scanSince = scanSince ?? Time.UtcNow;
        var jsonString = ScanDatabase<string>(@"
            SELECT PayloadJson 
            FROM MailHistory 
            WHERE SendDateTime > @scanSince
                AND `To` LIKE @email", new { scanSince, email });
        var message = JsonConvert.DeserializeObject<RemindPasswordMailMessage>(jsonString);
        string url = ConfigurationManager.GetValue("Urls:ApplicationUrl");
        return $"{url}service/account/{message!.TargetGuid}/RemindPassword/{message!.AuthCode}";
    }

    private static T ScanDatabase<T>(string query, object? values = null, int timeoutInSeconds = 60)
    {
        using var connection = new MySqlConnection(ConfigurationManager.GetValue("MysqlConnectionString"));
        connection.Open();
        DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);
        T? item;
        do
        {
            item = connection.QueryFirstOrDefault<T>(query, values);
            if (timeout < DateTime.UtcNow)
                throw new TimeoutException("Database entity not found");

        } while (item is null);
        return item;
    }
}
