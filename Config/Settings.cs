
using BackOnWay.Models;
using BackOnWay.Utils;

namespace BackOnWay.Config
{
    public class Settings
    {
        public string DbConnectionString { get; set; }

        public string GetDbString()
        {
            var binder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration configuration = binder.Build();

            var SettingInfoDb = configuration.GetSection("Settings").Get<Settings>();

            string ConnectionString = SettingInfoDb.DbConnectionString;

            return ConnectionString;
        }

        public SmtpSettings GetSmtpSettings()
        {
            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            SmtpSettings smtpSettings = config.GetSection("SmtpSettings").Get<SmtpSettings>();

            return smtpSettings;
        }

        public JwtSettings GetJwtSettings()
        {
            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            JwtSettings jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();

            return jwtSettings;
        }
    }
}
