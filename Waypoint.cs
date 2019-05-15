using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [TextArea(15,20)]
    public string text;
    public ShipShake ship;
    public WaypointManager manager;

    void OnTriggerStay(Collider coll){
        if(coll.tag == "Player"){
            Debug.Log("Ship is inside");
            if(!ship.sailsDown && ship.anchorDown){
                manager.StartFact(text);
                Debug.Log("Ship is anchored");
            }
        }
    }

    void OnTriggerExit(Collider coll){
        if(coll.tag == "Player"){
            manager.factsText.text = "";
        }
    }
}
