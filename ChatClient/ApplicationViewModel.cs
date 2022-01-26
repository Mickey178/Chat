using Chat.Common;
using ChatClient.ServiceReference1;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace ChatClient
{
    public class ApplicationViewModel : PropChanged
    {
        public int ID;

        private readonly DispatcherTimer timer;

        public RelayCommand SendMessageCommand { get; }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserMessageDto> Chat { get; set; }

        public ApplicationViewModel() { }

        public ApplicationViewModel(int ID)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal, Application.Current.Dispatcher)
            {
                Interval = new System.TimeSpan(0, 0, 0, 0, 500)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            Chat = new ObservableCollection<UserMessageDto>();
            SendMessageCommand = new RelayCommand(Send);
            this.ID = ID;
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            Chat.Clear();

            try
            {
                using (var client = new ServiceChatClient())
                {
                    foreach (var item in client.GetChatHistory())
                    {
                        Chat.Add(item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("error connect");
                Application.Current.Shutdown();
            }
        }

        private void Send(object obj)
        {
            using (var client = new ServiceChatClient())
            {
                client.SendMsg(Message, ID);
            }

            Message = "";
        }

        public void DisconnectUser()
        {
            using (var client = new ServiceChatClient())
            {
                client.Disconnect(ID);
            }
        }
    }
}