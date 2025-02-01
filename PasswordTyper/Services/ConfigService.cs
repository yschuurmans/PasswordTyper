using Encryption;
using Newtonsoft.Json;
using PasswordTyper.Forms;
using PasswordTyper.Models;

namespace PasswordTyper.Services
{
    public class ConfigService
    {
        // The file name of the encrypted config file. The file should be stored in %AppData%\PasswordTyper.
        private const string EncryptedConfigFileName = "PasswordTyperConfig.enc";

        private readonly string EncryptedConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordTyper");
        private readonly string EncryptedConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordTyper", EncryptedConfigFileName);

        // The password used to encrypt the data in memory. This password is not stored on disk.
        private byte[] VolatileEncryptionPassword { get; set; }

        // The encrypted data stored in memory. This data is decrypted using the VolatileEncryptionPassword.
        private string? EncryptedData { get; set; }

        // A simple "OK" string encrypted with the decryption key. Used to verify the decryption key upon saving new data.
        private string? EncryptionKeyVerification { get; set; }


        public ConfigService()
        {
            VolatileEncryptionPassword = Array.Empty<byte>();
        }

        public bool ConfigExists()
        {
            return File.Exists(EncryptedConfigFilePath);
        }

        public bool VerifyPassword(string decryptionKey)
        {
            string encryptedConfig = File.ReadAllText(EncryptedConfigFilePath);
            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(encryptedConfig, decryptionKey);
            return VerifyData(decryptedConfig);
        }

        public bool DecryptData(string decryptionKey)
        {
            // Decrypt the EncryptedConfig file using the decryptionKey.
            // Re-encrypt the EncryptedConfig file using the VolatileEncryptionPassword.
            // Store the EncryptedData in memory.
            string encryptedConfig = File.ReadAllText(EncryptedConfigFilePath);
            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(encryptedConfig, decryptionKey);
            if (!VerifyData(decryptedConfig))
            {
                return false;
            }

            EncryptionKeyVerification = AESThenHMAC.SimpleEncryptWithPassword("OK", decryptionKey);
            VolatileEncryptionPassword = AESThenHMAC.NewKey();
            EncryptedData = AESThenHMAC.SimpleEncryptWithPassword(decryptedConfig, Convert.ToBase64String(VolatileEncryptionPassword));

            return true;
        }

        public bool EncryptData(string decryptionKey)
        {
            if (EncryptedData == null || EncryptionKeyVerification == null)
            {
                throw new InvalidOperationException("Data is not decrypted.");
            }

            if (AESThenHMAC.SimpleDecryptWithPassword(EncryptionKeyVerification ?? "", decryptionKey) != "OK")
            {
                return false;
            }

            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(EncryptedData, Convert.ToBase64String(VolatileEncryptionPassword));
            if (!VerifyData(decryptedConfig))
            {
                throw new InvalidOperationException("Somehow, the data could not be decrypted anymore. Restart the application and try again.");
            }

            File.WriteAllText(EncryptedConfigFilePath, EncryptedData);
            return true;
        }


        public bool IsDecrypted()
        {
            return EncryptedData != null;
        }

        public ApplicationData? GetApplicationData(string processName, string windowTitle)
        {
            if (EncryptedData == null)
            {
                throw new InvalidOperationException("Data is not decrypted.");
            }

            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(EncryptedData, Convert.ToBase64String(VolatileEncryptionPassword));
            var config = JsonConvert.DeserializeObject<Config>(decryptedConfig);
            if (config == null)
            {
                throw new InvalidOperationException("Config is null.");
            }

            return config.Applications.FirstOrDefault(app => app.ProcessName == processName && app.WindowTitle == windowTitle);
        }

        private bool VerifyData(string decryptedConfig)
        {
            if (decryptedConfig == null) return false;

            try
            {
                var result = JsonConvert.DeserializeObject<Config>(decryptedConfig);
                if (result?.Verification == "OK")
                {
                    return true;
                }
            }
            catch (JsonReaderException)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        ///  Create a new config file with the given master password.
        /// </summary>
        /// <param name="masterPassword"></param>
        internal void InitializeNewConfig(string masterPassword)
        {
            var config = new Config
            {
                Verification = "OK",
                Applications = new List<ApplicationData>()
            };

            var encryptedConfig = AESThenHMAC.SimpleEncryptWithPassword(JsonConvert.SerializeObject(config), masterPassword);
            Directory.CreateDirectory(EncryptedConfigDirectory);
            File.WriteAllText(EncryptedConfigFilePath, encryptedConfig);

            VolatileEncryptionPassword = AESThenHMAC.NewKey();
            EncryptedData = AESThenHMAC.SimpleEncryptWithPassword(JsonConvert.SerializeObject(config), Convert.ToBase64String(VolatileEncryptionPassword));
            EncryptionKeyVerification = AESThenHMAC.SimpleEncryptWithPassword("OK", masterPassword);
        }

        internal void SaveConfig(Config config, string masterPassword)
        {
            var encryptedConfig = AESThenHMAC.SimpleEncryptWithPassword(JsonConvert.SerializeObject(config), masterPassword);
            File.WriteAllText(EncryptedConfigFilePath, encryptedConfig);

            VolatileEncryptionPassword = AESThenHMAC.NewKey();
            EncryptedData = AESThenHMAC.SimpleEncryptWithPassword(JsonConvert.SerializeObject(config), Convert.ToBase64String(VolatileEncryptionPassword));
            EncryptionKeyVerification = AESThenHMAC.SimpleEncryptWithPassword("OK", masterPassword);
        }

        internal bool AddOrEditApplication(ApplicationData application)
        {
            if (EncryptedData == null)
            {
                throw new InvalidOperationException("Data is not decrypted.");
            }

            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(EncryptedData, Convert.ToBase64String(VolatileEncryptionPassword));
            var config = JsonConvert.DeserializeObject<Config>(decryptedConfig);
            if (config == null)
            {
                throw new InvalidOperationException("Config is null.");
            }

            var existingApp = config.Applications.FirstOrDefault(app => app.ProcessName == application.ProcessName && app.WindowTitle == application.WindowTitle);
            if (existingApp != null)
            {
                existingApp.TypeUsername = application.TypeUsername;
                existingApp.Username = application.Username;
                existingApp.TypePassword = application.TypePassword;
                existingApp.Password = application.Password;
                existingApp.TypeTotp = application.TypeTotp;
                existingApp.TotpSecret = application.TotpSecret;
            }
            else
            {
                config.Applications.Add(application);
            }

            // Re-encrypt the data
            var masterPassword = PasswordPrompt.PromptPassword();
            if (masterPassword.correct)
            {
                SaveConfig(config, masterPassword.password);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal IEnumerable<ApplicationData> GetApplicationList()
        {
            if (EncryptedData == null)
            {
                throw new InvalidOperationException("Data is not decrypted.");
            }

            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(EncryptedData, Convert.ToBase64String(VolatileEncryptionPassword));
            var config = JsonConvert.DeserializeObject<Config>(decryptedConfig);
            if (config == null)
            {
                throw new InvalidOperationException("Config is null.");
            }

            return config.Applications;
        }

        internal void ChangePassword(string currentEncryptionKey, string newEncryptionKey)
        {
            // Decrypt the EncryptedConfig file using the currentEncryptionKey.
            // Re-encrypt the EncryptedConfig file using the newEncryptionKey.
            string encryptedConfig = File.ReadAllText(EncryptedConfigFilePath);
            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(encryptedConfig, currentEncryptionKey);
            if (!VerifyData(decryptedConfig))
            {
                throw new InvalidOperationException("Data could not be decrypted.");
            }
            var config = JsonConvert.DeserializeObject<Config>(decryptedConfig);
            if (config == null)
            {
                throw new InvalidOperationException("Config is null.");
            }
            SaveConfig(config, newEncryptionKey);
        }

        internal void DeleteApplication(ApplicationData application, string masterPassword)
        {
            if (EncryptedData == null)
            {
                throw new InvalidOperationException("Data is not decrypted.");
            }

            var decryptedConfig = AESThenHMAC.SimpleDecryptWithPassword(EncryptedData, Convert.ToBase64String(VolatileEncryptionPassword));
            var config = JsonConvert.DeserializeObject<Config>(decryptedConfig);
            if (config == null)
            {
                throw new InvalidOperationException("Config is null.");
            }

            var existingApp = config.Applications.FirstOrDefault(app => app.ProcessName == application.ProcessName && app.WindowTitle == application.WindowTitle);
            if (existingApp != null)
            {
                config.Applications.Remove(existingApp);
            }

            // Re-encrypt the data
            SaveConfig(config, masterPassword);
        }
    }
}
