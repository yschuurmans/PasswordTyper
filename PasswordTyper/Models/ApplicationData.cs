﻿using Newtonsoft.Json;

namespace PasswordTyper.Models
{
    public class ApplicationData : IEquatable<ApplicationData>
    {
        public string FriendlyName { get; set; }
        public string WindowTitle { get; set; }
        public string ProcessName { get; set; }

        public bool TypeUsername { get; set; }
        public string Username { get; set; }

        public bool TypePassword { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string MaskedPassword => new string('*', Password.Length);

        public bool TypeTotp { get; set; }
        public string TotpSecret { get; set; }
        [JsonIgnore]
        public string MaskedTotpSecret => new string('*', TotpSecret.Length);

        public bool Equals(ApplicationData? other)
        {
            if (other == null)
                return false;

            return FriendlyName == other.FriendlyName &&
                   WindowTitle == other.WindowTitle &&
                   ProcessName == other.ProcessName &&
                   TypeUsername == other.TypeUsername &&
                   Username == other.Username &&
                   TypePassword == other.TypePassword &&
                   Password == other.Password &&
                   TypeTotp == other.TypeTotp &&
                   TotpSecret == other.TotpSecret;
        }
    }
}
