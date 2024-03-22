using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public float transitionTime = 0.2f;

    public TMP_Dropdown filter;
    private API_Caller apiCall;

    public GameObject DetailMenu;
    
    private void Start()
    {
        apiCall = GetComponent<API_Caller>();
        instance = this;
    }
    //function for opening popup menu
    public void DetailMenuOpen()
    {
        DetailMenu.transform.DOScale(1, transitionTime);
    }
    //function for closing popup menu
    public void DetailMenuClose()
    {
        DetailMenu.transform.DOScale(0, transitionTime);
    }
    //function for changing filter with dropdown menu
    public void OnFilterChange()
    {
        apiCall.SortList(filter.value);
    }

}
