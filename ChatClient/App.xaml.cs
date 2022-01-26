using System.Windows;

namespace ChatClient
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var registrationWindow = new RegistrationWindow();
            RegistrationViewModel registViewModel = new RegistrationViewModel();
            registrationWindow.DataContext = registViewModel;
            registrationWindow.ShowDialog();

            if (!registViewModel.RegistrationResult)
            {
                Shutdown();
                return;
            }

            MainWindow mainWindow = new MainWindow
            {
                DataContext = new ApplicationViewModel(registViewModel.ID)
            };

            mainWindow.ShowDialog();
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ApplicationViewModel appViewModel = new ApplicationViewModel();
            appViewModel.DisconnectUser();
        }
    }
}
