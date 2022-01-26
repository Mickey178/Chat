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
            var messagesFromTheDB = new List<UserMessage>();
            using (var db = new ApplicationContext())
            {
                foreach (var item in db.UserMessages)
                {
                    messagesFromTheDB.Add(item);
                }
            }

            var messagesFromDbDto = new List<UserMessageDto>();
            foreach (var item in messagesFromTheDB)
            {
                messagesFromDbDto.Add(new UserMessageDto
                {
                    Date = item.Date,
                    Name = item.Name,
                    Message = item.Message
                });
            }
            return messagesFromDbDto;
        }

        public void SendMsg(string msg, int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);

            using (var db = new ApplicationContext())
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
