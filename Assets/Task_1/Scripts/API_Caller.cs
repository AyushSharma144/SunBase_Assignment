using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json;
using DG.Tweening;

public class API_Caller : MonoBehaviour
{
    string url = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public GameObject clientPrefab;
    public Transform uiPanel;
    //list of ui gameobjects with client data
    public List<GameObject> clientList = new List<GameObject>();


    void Start()
    {
        StartCoroutine(GetRequest(url));
    }
    //coroutine for requesting json file 
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                ParseJSONAndInstantiate(json);
            }
        }
    }
    //parsing json file and spawning ui gamneobjects
    void ParseJSONAndInstantiate(string jsonString)
    {
        ClientDataContainer dataContainer = JsonConvert.DeserializeObject<ClientDataContainer>(jsonString);

        foreach (var clientData in dataContainer.clients)
        {
            GameObject newClient = Instantiate(clientPrefab, uiPanel);

            clientList.Add(newClient);

            ClientMenuScript clientScript = newClient.GetComponent<ClientMenuScript>();

            clientScript.isManager = clientData.isManager;
            clientScript.id = clientData.id;
            clientScript.label = clientData.label;

            if (dataContainer.data.ContainsKey(clientData.id.ToString()))
            {
                var additionalData = dataContainer.data[clientData.id.ToString()];
                clientScript.address = additionalData.address;
                clientScript.Name = additionalData.name;
                clientScript.points = additionalData.points;
            }
        }
        SortList(0);
    }
    //function for filter
    public void SortList(int Case)
    {
        foreach(GameObject client in clientList)
        {
            ClientMenuScript data = client.GetComponent<ClientMenuScript>();
            if (Case <= 0)
            {
                client.SetActive(true);
                client.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            }
            else if (Case == 1)
            {
                if(data.isManager)
                {
                    client.SetActive(true);
                    client.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
                }
                else
                {
                    client.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() => { client.SetActive(false); });
                }
            }
            else
            {
                if (!data.isManager)
                {
                    client.SetActive(true);
                    client.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
                }
                else
                {
                    client.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(()=> { client.SetActive(false); });
                }
            }
        }
    }
}
//classes needed for parsing 
[System.Serializable]
public class ClientData
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class AdditionalClientData
{
    public string address;
    public string name;
    public int points;
}

[System.Serializable]
public class ClientDataContainer
{
    public List<ClientData> clients;
    public Dictionary<string, AdditionalClientData> data;
    public string label;
}
