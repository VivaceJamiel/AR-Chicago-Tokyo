using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;


public class chicagoWeatherAPI : MonoBehaviour
{
    string url = "https://api.openweathermap.org/data/2.5/weather?q=Tokyo&appid=20e90bbe908bc85c798290f956bce23e";

    public GameObject weatherText;

    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void GetDataFromWeb()
    {

        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
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
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                Debug.Log(":\nRaw: " + webRequest);
                var data = webRequest.downloadHandler.text;
                Debug.Log(data);
            }
        }
    }
}