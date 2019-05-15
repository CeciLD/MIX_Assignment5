using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask mask;
    public ShipShake shipControls;

    LineRenderer line;
    public WaypointManager waypointManager;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetColors(Color.green, Color.green);
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask)){
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, hit.point);
            line.SetColors(Color.green, Color.green);
            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Space))
            {
                if(hit.transform.tag == "Sails"){
                    shipControls.ChangeSailsState();
                }else if(hit.transform.tag == "Anchor"){
                    shipControls.ChangeAnchorState();
                }else if (hit.transform.tag == "Close"){
                    waypointManager.CloseCanvas();
                }
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

            }            
        }
        else
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 25);
            line.SetColors(Color.cyan, Color.cyan);
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        if(Input.GetKeyDown(KeyCode.S)){
            shipControls.ChangeSailsState();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            shipControls.ChangeAnchorState();
        }

        if((Input.GetKey(KeyCode.RightArrow)) && !shipControls.anchorDown){
            shipControls.SteerRight();
        }
        if((Input.GetKey(KeyCode.LeftArrow)) && !shipControls.anchorDown){
            shipControls.SteerLeft();
        }

        if(OVRInput.GetDown(OVRInput.Button.Back)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
