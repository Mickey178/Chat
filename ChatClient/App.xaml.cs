using System.Windows;

namespace ChatClient
{
    public partial class App : Application
    {
        private Window mainWindow;

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

            mainWindow = new MainWindow
            {
                DataContext = new ApplicationViewModel(registViewModel.ID)
            };

            mainWindow.ShowDialog();
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (mainWindow != null)
            {
                ((ApplicationViewModel)mainWindow.DataContext).DisconnectUser();
            }
            base.OnExit(e);
        }
    }
}
