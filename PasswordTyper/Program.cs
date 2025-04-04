using NuGet.Versioning;
using PasswordTyper.Helpers;
using Velopack;
using Velopack.Sources;

namespace PasswordTyper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VelopackApp.Build()
                .WithFirstRun(FirstRun)
                .Run();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());


#if !DEBUG
            UpdateApp().Wait();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApp());

        }

        private static void FirstRun(SemanticVersion version)
        {
            StartupHelper.AddApplicationToStartup();
        }

        private static async Task UpdateApp()
        {
            var mgr = new UpdateManager(new GithubSource("https://github.com/yschuurmans/PasswordTyper", null, false));

            // check for new version
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
                return; // no update available

            // download new version
            await mgr.DownloadUpdatesAsync(newVersion);

            // install new version and restart app
            mgr.ApplyUpdatesAndRestart(newVersion);
        }
    }
}