using System.Collections.Generic;
using System.ServiceModel;

namespace Chat.Common
{
    [ServiceContract]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);

        [OperationContract]
        List<UserMessageDto> GetChatHistory();
    }
}
