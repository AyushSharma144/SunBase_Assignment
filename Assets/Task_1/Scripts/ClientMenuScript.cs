using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientMenuScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI listData;

    public bool isManager;
    public int id;
    public string label = "Not Available";
    public string address = "Not Available";
    public string Name = "Not Available";
    public int points;

    private void Start()
    {
        listData.text = id + ": Label: " + label + " points: " + points.ToString();
    }

    public void PopupClick()
    {
        PopPupMenu.instance.ClientInfo(Name, address, points);
    }
}

