using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;

public class ChicagoTime : MonoBehaviour
{
    public GameObject timeTextObject;
    string chicagoURL = "http://worldtimeapi.org/api/timezone/America/Chicago";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 0.1f, 60f);
    }

    void GetDataFromWeb()
    {
        StartCoroutine(UpdateTime(chicagoURL));
    }

    IEnumerator UpdateTime(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                var chicagoData = webRequest.downloadHandler.text;
                var subString = chicagoData.IndexOf('T', 30);
                var datetime = chicagoData.Substring(subString + 1, 21);
                var time = DateTime.Parse(datetime).ToString("hh:mm tt");
                timeTextObject.GetComponent<TextMeshPro>().text = time;
            }
        }
    }
}