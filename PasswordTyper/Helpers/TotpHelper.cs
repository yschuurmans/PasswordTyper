using OtpNet;

namespace PasswordTyper.Helpers
{
    public static class TotpHelper
    {
        public static string GenerateTotp(string secret)
        {
            var secretBytes = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretBytes);
            return totp.ComputeTotp();
        }
    }
}
