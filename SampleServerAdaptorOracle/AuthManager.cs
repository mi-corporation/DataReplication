using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCo.MiApp.DataReplication.ImportApi;
using MiCo.MiApp.DataReplication.Messages;


namespace SampleServerAdaptorOracle {
    public class AuthManager {
        public readonly IEncryptedCredentialService credService;
        private readonly AuthenticationApi authApi;

        public string UserName { get; set; }
        public string ClientKey { get; set; }
        public string PublicKey { get; set; }
        public string EncryptedPassword { get; set; }

        public AuthManager(Uri baseUri) {
            this.credService = new EncryptedCredentialsService();
            this.authApi = new AuthenticationApi(baseUri);
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ClientKey = configuration.AppSettings.Settings["ClientKey"].Value;
            PublicKey = configuration.AppSettings.Settings["PublicKey"].Value;
            EncryptedPassword = configuration.AppSettings.Settings["EncryptedPassword"].Value;
        }
        public async Task SetCredentials(string userName, string password) {
            UserName = userName;
            //// Returns client key & public key 
            EncryptPasswordResponse results = await authApi.RegisterAndGetPublicKey();

            //// Encrypt the password
            RsaEncryptionService encryptionService = new RsaEncryptionService { PublicKey = results.PublicKey };
            EncryptedPassword = encryptionService.EncryptString(password); 

            ClientKey = results.ClientKey;
            PublicKey = results.PublicKey;
            
            //// Store client key, encrypted pw & public key
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["ClientKey"].Value = results.ClientKey;
            configuration.AppSettings.Settings["PublicKey"].Value = results.PublicKey;
            configuration.AppSettings.Settings["EncryptedPassword"].Value = EncryptedPassword;
            configuration.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
