using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;

public class TokyoTime : MonoBehaviour
{
    public GameObject timeTextObject;
    string tokyoURL = "http://worldtimeapi.org/api/timezone/Asia/Tokyo";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 0.1f, 60f);
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
                var tokyoData = webRequest.downloadHandler.text;
                var time = tokyoData.Substring(97, 5);
                timeTextObject.GetComponent<TextMeshPro>().text = time;
            }
        }
    }
}