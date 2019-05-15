using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public ShipShake ship;
    public GameObject canvas;

    private int waypointNum = 0;

    public Text factsText;

    void Start(){
        waypoints[0].SetActive(true);
        Debug.Log(waypoints.Length);
    }

    public void StartFact(string text){
        canvas.SetActive(true);
        factsText.text = text;
        waypoints[waypointNum].SetActive(false);
        waypointNum++;
        if(waypointNum < waypoints.Length){
            waypoints[waypointNum].SetActive(true);
        }
    }

    public void CloseCanvas(){
        canvas.SetActive(false);
    }
}
