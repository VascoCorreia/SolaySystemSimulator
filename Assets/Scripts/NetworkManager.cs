using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(planetData());
    }
    public void resetGame()
    {
        StartCoroutine(postResetGame());
    }

    public void getPlanetData()
    {
        StartCoroutine(planetData());
    }

    public IEnumerator postResetGame()
    {
        // POST
        //var dataToPost = new PostData() { Hero = "John Wick", PowerLevel = 9001 };
        var postRequest = CreateRequest("http://localhost:3300/resetGame", RequestType.POST, null);
        yield return postRequest.SendWebRequest();

    }

    public IEnumerator planetData()
    {
        //// GET
        var getRequest = CreateRequest("http://localhost:3300/getAllPlanetsData", RequestType.GET);
        yield return getRequest.SendWebRequest();
        var deserializedGetData = JsonUtility.FromJson<CelestialBodyDataWrapper>(getRequest.downloadHandler.text);
        Debug.Log(deserializedGetData);
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }
}

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}


public class Todo
{
    // Ensure no getters / setters
    // Typecase has to match exactly
    public int userId;
    public int id;
    public string title;
    public bool completed;
}

public class PostResult
{
    public string success { get; set; }
}