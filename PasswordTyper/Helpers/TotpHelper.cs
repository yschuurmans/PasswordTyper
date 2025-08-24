using GuerrillaNtp;
using OtpNet;
using Polly;

namespace PasswordTyper.Helpers
{
    public static class TotpHelper
    {
        private static NtpClock? _clock;
        public static string GenerateTotp(string secret)
        {
            var secretBytes = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretBytes);
            DateTime utcTime = DateTime.UtcNow;

            if (_clock == null)
            {
                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(5, retryAttempt => TimeSpan.FromMilliseconds(200));

                try
                {
                    policy.Execute(() =>
                    {
                        NtpClient client = NtpClient.Default;
                        _clock = client.Query();
                    });

                    if (_clock != null)
                    {
                        utcTime = _clock.UtcNow.DateTime;
                        return totp.ComputeTotp(utcTime);
                    }
                }
                catch
                {
                    return totp.ComputeTotp();
                }
            }

            if (_clock != null)
            {
                utcTime = _clock.UtcNow.DateTime;
            }

            return totp.ComputeTotp(utcTime);
        }
    }
}
