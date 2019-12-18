using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
    //创建单例类PhotonEngine
    private static PhotonEngine Instance;

    private static PhotonPeer peer;

    public static PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }

    private void Awake()
    {
        //保持所有场景中只有一个PhotonEngine
        if (Instance == null)
        {
            Instance = this;
            //跳转场景等不销毁
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //通过Listener接受服务器端的响应，链接协议
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGame1");

    }

    // Update is called once per frame
    void Update()
    {
        peer.Service(); //维持服务链接
    }

    private void OnDestroy()
    {
        if (peer!=null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();  //断开连接
        }
    }

    //IPhotonPeerListener接口方法

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }
    //服务器端向客户端直接发请求或通知在这里接受
    public void OnEvent(EventData eventData)
    {
        throw new System.NotImplementedException();
    }
    //客户端发起请求后，服务器的响应在这里接收
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("收到了服务器端的响应Type1");
                Dictionary<byte, object> data = operationResponse.Parameters;    //接收数据
                object intValue;
                data.TryGetValue(1, out intValue);
                object stringValue;
                data.TryGetValue(2, out stringValue);
                Debug.Log("接收到服务器返回的数据1："+ intValue + " 2："+ stringValue);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
    //状态改变后触发方法
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    //IPhotonPeerListener接口方法end
}
