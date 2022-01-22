using Chat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private readonly List<ServerUser> users = new List<ServerUser>();
        private int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                OperationContext = OperationContext.Current
            };
            nextId++;

            users.Add(user);

            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public List<UserMessageDto> GetChatHistory()
        {
            List<UserMessage> list = new List<UserMessage>();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in db.UserMessages)
                {
                    list.Add(item);
                }
            }
            List<UserMessageDto> listDto = new List<UserMessageDto>();
            foreach (var item in list)
            {
                listDto.Add(new UserMessageDto
                {
                    Date = item.Date,
                    Name = item.Name,
                    Message = item.Message
                }) ;
            }
            return listDto;
        }

        public void SendMsg(string msg, int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);

            using (ApplicationContext db = new ApplicationContext())
            {
                db.UserMessages.Add(new UserMessage
                {
                    Date = DateTime.Now,
                    Name = user.Name,
                    Message = msg
                });
                db.SaveChanges();
            }
        }
    }
}
