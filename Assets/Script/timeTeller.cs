using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;

public class timeTeller : MonoBehaviour
{
    public GameObject timeTextObject;
    string tokyoURL = "http://worldtimeapi.org/api/timezone/Asia/Tokyo.txt";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void GetDataFromWeb()
    {
        StartCoroutine(UpdateTime(tokyoURL));
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
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                //Debug.Log(":\nReceived: " + webRequest);
                var data = webRequest.downloadHandler.text;
                Debug.Log(data);
            }
        }
        timeTextObject.GetComponent<TextMeshPro>().text = DateTime.Now.ToString("h:mm tt");
    }
}