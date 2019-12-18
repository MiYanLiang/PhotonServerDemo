using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendTest : MonoBehaviour
{
    [SerializeField]
    Text inputText;

    public void TakeBtnFun()
    {
        Debug.Log("向服务器发送了请求");
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 000);
        data.Add(2, inputText.text);
        //发送请求(请求类型, 数据, 可靠传输)
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
