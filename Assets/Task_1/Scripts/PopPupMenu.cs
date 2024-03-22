using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopPupMenu: MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI addressText;

    public static PopPupMenu instance;

    private void Start()
    {
        instance = this;
        this.transform.localScale = Vector3.zero;
    }
    public void ClientInfo(string name,string address,int points)
    {
        nameText.text = "Name: " + name;
        addressText.text = "Address: " + address;
        pointText.text = "Points: " + points.ToString();
        UIManager.instance.DetailMenuOpen();
    }


}
