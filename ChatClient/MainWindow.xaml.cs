using System.Windows;

namespace ChatClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ApplicationViewModel)DataContext).Disconnect();
        }
    }
}
