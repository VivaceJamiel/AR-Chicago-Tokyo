using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class tokyoWeatherAPI : MonoBehaviour
{
    string url = "https://api.openweathermap.org/data/2.5/weather?q=Tokyo&appid=20e90bbe908bc85c798290f956bce23e&units=metric";

    public GameObject weatherText;

    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 600f);
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
                var data = webRequest.downloadHandler.text;

                // Get what the weather is
                var weatherResult = JObject.Parse(data).GetValue("weather").ToString();
                weatherResult = weatherResult.Substring(1, weatherResult.Length - 3);
                var weatherData = JObject.Parse(weatherResult).GetValue("main").ToString();

                // Get the temperature
                var tempResult = JObject.Parse(data).GetValue("main").ToString();
                var tempData = JObject.Parse(tempResult).GetValue("temp");
                var decTemp = Convert.ToDecimal(tempData);
                var intTemp = decimal.Round(decTemp);

                var weatherString = intTemp + "ÅãC\n" + weatherData;

                Debug.Log(weatherString);

                weatherText.GetComponent<TextMeshPro>().text = weatherString;
            }
        }
    }
}