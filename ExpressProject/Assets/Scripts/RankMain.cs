using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class RankMain : MonoBehaviour
{
    public string host;
    public int port;
    public string top3Uri;
    public string idUri;
    public string postUri;
    public string id;
    public int score;

    public Button BtnGetTop3;
    public Button BtnGetId;
    public Button BtnPost;

    private void Start()
    {
        this.BtnGetId.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, idUri + "/" + id);
            Debug.Log(url);

            StartCoroutine(this.GetId(url, (raw) =>
             {
                 var res = JsonUtility.FromJson<Protocols.Packets.res_scores_id>(raw);
                 Debug.LogFormat("{0}, {1}", res.result.id, res.result.score);
             }));
        });

        this.BtnGetId.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
            Debug.Log(url);

            StartCoroutine(this.GetId(url, (raw) =>
            {
                var res = JsonUtility.FromJson<Protocols.Packets.res_scores_id>(raw);
                //foreach(var user in res.result)
                //{
                //    Debug.LogFormat("{0}, {1}", user.id, user.score);
                //}
            }));
        });

        this.BtnPost.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, postUri);
            Debug.Log(url);

            var req = new Protocols.Packets.req_scores();
            req.cmd = 1000;
            req.id = id;
            req.score = score;
            var json = JsonUtility.ToJson(req);
            Debug.Log(json);

            StartCoroutine(this.PostScore(url, json, (raw) =>
            {
                Protocols.Packets.res_scores res = JsonUtility.FromJson<Protocols.Packets.res_scores>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });

    }

    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);                                 //����ȭ(���ڿ� -> ����Ʈ�迭)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();                                   //Node.js�� ����
        
        if(webRequest.result == UnityWebRequest.Result.ConnectionError ||           //���� ��Ʈ��ũ ���� ���� äŷ
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� ����");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}\n", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetId(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("----->" + webRequest.downloadHandler.text);

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||          //���� ��Ʈ��ũ ���� ���� äŷ
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� ����");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||          //���� ��Ʈ��ũ ���� ���� äŷ
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� ����");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }
}
