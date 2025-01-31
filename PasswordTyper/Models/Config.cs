namespace PasswordTyper.Models
{
    internal class Config
    {
        public Config()
        {
            Verification = string.Empty;
            Applications = new List<ApplicationData>();
        }

        public string Verification { get; set; }
        public List<ApplicationData> Applications { get; set; }
    }
}
