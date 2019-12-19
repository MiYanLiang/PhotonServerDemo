using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace MyGameServer
{
    //与客户端的交互类
    public class ClientPeer:Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest)
            : base(initRequest)
        {

        }

        //断开连接时
        protected override void OnDisconnect(PhotonHostRuntimeInterfaces.DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.log.Info("有一个客户端断开了连接");
        }

        //接受请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode) //通过code区分请求类型
            {
                case 1:
                    MyGameServer.log.Info("收到了一个客户端的请求Type1");

                    Dictionary<byte, object> data = operationRequest.Parameters;    //接收数据
                    object intValue;
                    data.TryGetValue(1, out intValue);
                    object stringValue;
                    data.TryGetValue(2, out stringValue);
                    MyGameServer.log.Info("得到客户端传递数据1："+intValue.ToString()+"  --  2："+stringValue.ToString());
                    OperationResponse opResponse = new OperationResponse(1);    //设置响应请求类型

                    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    data2.Add(1, 111);
                    data2.Add(2, "这是服务器返回字符串");
                    opResponse.Parameters = data2;
                    SendOperationResponse(opResponse, sendParameters);  //发给客户端响应
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
