using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiRequests 
{
    public static CelestialBodyData getPlanetData(int planetID)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/getPlanetData?id=" + planetID);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<CelestialBodyData>(json);
    }
}
