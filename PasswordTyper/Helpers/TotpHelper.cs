using GuerrillaNtp;
using OtpNet;

namespace PasswordTyper.Helpers
{
    public static class TotpHelper
    {
        private static NtpClock? _clock;
        public static string GenerateTotp(string secret)
        {
            var secretBytes = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretBytes);
            if (_clock == null)
            {
                NtpClient client = NtpClient.Default;
                _clock = client.Query();
            }

            var utcTime = _clock.UtcNow.DateTime;

            return totp.ComputeTotp(utcTime);
        }
    }
}
