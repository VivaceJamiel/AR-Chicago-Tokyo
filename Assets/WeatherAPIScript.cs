using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class WeatherAPIScript : MonoBehaviour
{
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=20e90bbe908bc85c798290f956bce23e&units=imperial";


    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds

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


            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}