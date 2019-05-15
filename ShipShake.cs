using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipShake : MonoBehaviour
{
    Vector3 floatY;
    float originalY;

    public float steeringAngle = 0;
    float angleY = 180;

    [Header("Bopping Y")]
    public float floatingStrength;
    public float floatingSpeed;
    [Header("Rotating Y")]
    public float rotatingStrength;
    public float rotatingSpeed;
    [Header("Rotating X")]
    public float rotatingStrengthX;
    public float rotatingSpeedX;
    [Header("UI")]
    public Text anchorStateText;
    public Text sailsStateText;

    //2, 1, 4, 0.5
    ///
    //[HideInInspector]
    public bool sailsDown = true;
    //[HideInInspector]
    public bool anchorDown = false;

    //sailing
    public float initialSpeed;
    private float speed;

    //
    public GameObject[] sails;

    public GameObject wheel;
    private float wheelAngle = 0;

    void Start()
    {
        originalY = this.transform.position.y;

        sailsStateText.text = "Up";
        sailsStateText.color = Color.red;

        anchorStateText.text = "Down";
        anchorStateText.color = Color.red;
    }

    void Update()
    {
        SpeedChange();

        transform.position = new Vector3(transform.position.x, originalY + 
        ((float)Mathf.Sin(Time.time * floatingSpeed) * floatingStrength ), transform.position.z);

        transform.rotation = Quaternion.Euler(rotatingStrengthX * Mathf.Sin(Time.time * rotatingSpeedX), angleY,
        rotatingStrength * Mathf.Sin(Time.time * rotatingSpeed));

        wheel.transform.localRotation = Quaternion.Euler(0, 0, -wheelAngle);

        if(sailsDown && !anchorDown){
            Sailing();
            Steer();
        }else{
            StopSailing();
        }

    }

    void SpeedChange(){
        if(anchorDown && !sailsDown){
            floatingStrength = 1;
            floatingSpeed = 1;
            rotatingStrength = 1;
            rotatingSpeed = 1;
            rotatingStrengthX = 1;
            rotatingSpeedX = 1;
        }
        else if(anchorDown && sailsDown){
            floatingStrength = 4;
            floatingSpeed = 1.3f;
            rotatingStrength = 5;
            rotatingSpeed = 1.8f;
            rotatingStrengthX = 1;
            rotatingSpeedX = 1;
        }
        else if (!anchorDown && sailsDown){
            floatingStrength = 2.4f;
            floatingSpeed = 1.8f;
            rotatingStrength = 1.6f;
            rotatingSpeed = 0.5f;
            rotatingStrengthX = 3;
            rotatingSpeedX = 2;
        }
        else if(!anchorDown && !sailsDown) {
            floatingStrength = 2;
            floatingSpeed = 1;
            rotatingStrength = 3;
            rotatingSpeed = 0.5f;
            rotatingStrengthX = 1;
            rotatingSpeedX = 1;
        }
    }

    public void ChangeSailsState(){
        if(sailsDown){
            sailsDown = false;
            foreach(GameObject o in sails){
                o.transform.localScale = new Vector3(1, 0.3f, 1);
            }
            sailsStateText.text = "Up";
            sailsStateText.color = Color.red;
        }
        else{
            sailsDown = true;
            foreach(GameObject o in sails){
                o.transform.localScale = new Vector3(1, 1, 1);
            }
            sailsStateText.text = "Down";
            sailsStateText.color = Color.green;
        }
    }

    public void ChangeAnchorState(){
        if(anchorDown){
            anchorDown = false;
            anchorStateText.text = "Up";
            anchorStateText.color = Color.green;
        }
        else{
            anchorDown = true;
            anchorStateText.text = "Down";
            anchorStateText.color = Color.red;
        }
    }

    void Sailing(){
        speed = initialSpeed;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void StopSailing(){
        speed = 0;
    }

    public void SteerRight(){
        angleY += steeringAngle;
    }

    public void SteerLeft(){
        angleY -= steeringAngle;
    }

    void Steer(){
        Vector2 dir = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if(dir.x > 0){
            angleY += steeringAngle;
            wheelAngle += steeringAngle * 20f;
        }
        else if (dir.x < 0){
            angleY -= steeringAngle;
            wheelAngle -= steeringAngle * 20f;
        }
    }
}
