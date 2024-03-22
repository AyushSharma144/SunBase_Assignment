using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GestureController : MonoBehaviour
{

    private TrailRenderer trail;
    private List<GameObject> circles = new List<GameObject>();

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }
    //update functions checks for mouse click
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            EnableTrail();
        }
        else
        {
            DisableTrail();
        }
    }
    //a function that makes objects follow curser
    private void EnableTrail()
    {
        Vector3 mousePos = GetMousePositionInWorld();
        trail.transform.position = mousePos;
        trail.enabled = true;
    }
    //a function that returns mouse position
    private Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    //checks for triggers and makes alist of  all the objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            circles.Add(collision.gameObject);
            Debug.Log("Hit");
        }
    }
    //disabling trail rendering and destroying all objects added to the list by triggering
    private void DisableTrail()
    {
        if (trail.enabled)
        {
            foreach (GameObject circle in circles)
            {
                circle.transform.DOScale(0, 0.1f).OnComplete(() =>
                 {
                     Destroy(circle);
                 });
            }
            circles.Clear();
            trail.enabled = false;
        }
    }

    
}
