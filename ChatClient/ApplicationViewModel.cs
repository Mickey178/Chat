using Chat.Common;
using ChatClient.ServiceReference1;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Threading;

namespace ChatClient
{
    public class ApplicationViewModel : PropChanged 
    {
        public bool isConnected = false;

        public int ID;

        private readonly DispatcherTimer timer;

        public RelayCommand ConnectUserCommand { get; }

        public RelayCommand DisconnectUserCommand { get; }

        public RelayCommand SendMessageCommand { get; }


        private string name = "Name user";

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<UserMessageDto> Chats { get; set; }

        public ApplicationViewModel()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            timer.Interval = new System.TimeSpan(0,0,0,0,500);
            timer.Tick += Timer_Tick;
            timer.Start();
            Chats = new ObservableCollection<UserMessageDto>();
            ConnectUserCommand = new RelayCommand(ConnectUser);
            DisconnectUserCommand = new RelayCommand(DisconnectUser);
            SendMessageCommand = new RelayCommand(Send);

        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (!isConnected)
            {
                return;
            }

            Chats.Clear();

            using (var client = new ServiceChatClient())
            {
                foreach (var item in client.GetChatHistory())
                {
                    Chats.Add(item);
                }
            }
        }

        private void Send(object obj)
        {
            if (isConnected)
            {
                using(var client = new ServiceChatClient())
                {
                    client.SendMsg(Message, ID);
                }

                Message = "";
            }
        }

        private void ConnectUser(object obj)
        {
            if (!isConnected)
            {
                using (var client = new ServiceChatClient())
                {
                    foreach (var item in client.GetChatHistory())
                    {
                        Chats.Add(item);
                    }
                    ID = client.Connect(name);
                }
                isConnected = true;
            }
        }

        private void DisconnectUser(object obj)
        {
            Disconnect();
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                using (var client = new ServiceChatClient())
                {
                    client.Disconnect(ID);
                }
                isConnected = false;
            }
        }
    }
}