using System;
using ChatClient.ServiceReference1;
using System.Windows;

namespace ChatClient
{
    public class RegistrationViewModel : PropChanged
    {
        public int ID { get; private set; }

        public RelayCommand ConnectUserCommand { get; }

        public bool RegistrationResult { get; private set; }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public RegistrationViewModel()
        {
            ConnectUserCommand = new RelayCommand(ConnectUser);
        }

        private void ConnectUser(object obj)
        {
            var RegistWindow = obj as RegistrationWindow;

            try
            {
                RegistrationResult = true;
                using (var client = new ServiceChatClient())
                {
                    ID = client.Connect(name);
                }

                RegistWindow.Close();
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                ActionAfterError(RegistWindow);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                ActionAfterError(RegistWindow);
            }
        }

        private void ActionAfterError(RegistrationWindow RegistWindow)
        {
            MessageBox.Show("error");
            RegistrationResult = false;
            RegistWindow.Close();
        }
    }
}
