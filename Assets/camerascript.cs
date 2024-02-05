using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Transform root;

    float mouseX, mouseY;

    public float stomachOffset;

    public ConfigurableJoint hipJoint, stomachJoint;

    // Update is called once per frame
    void FixedUpdate()
    {
        CamControl();
    }

    public void CamControl(){
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);

        root.rotation = rootRotation;

    
    }
}