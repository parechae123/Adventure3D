using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam 
{
    Transform camTR;
    Transform playerTR;
    float mouseSensitivity = 0.1f;
    float xAxis = 0f;
    float xMinAxis = -180f;
    float XAxis
    {
        get { return xAxis; }
        set 
        {
            if(value > Mathf.Abs(xMinAxis)) value -= 360f;
            else if(value < xMinAxis) value += 360f;
            xAxis = value;
        }
    }
    float yAxis = 0f;
    float yMinAxis = -45f;
    float YAxis
    {
        get { return yAxis; }
        set { yAxis = Mathf.Clamp(value, yMinAxis, Mathf.Abs(yMinAxis)); }
    }
    public PlayerCam(Transform tr)
    {
        playerTR = tr;
        camTR = Camera.main.transform;
    }
    // Update is called once per frame
    public void RotCamera(Vector2 vec)
    {
        SetXRotation(vec.y);
        SetYRotation(vec.x);
        SetYPos(YAxis);

    }
private void SetXRotation(float yAxis)
    {
        YAxis -= yAxis* (mouseSensitivity/2f);
        camTR.localEulerAngles = new Vector3(YAxis, 0f, 0f);
        /*XAxis += Input.GetAxis("Mouse X");
        YAxis -= Input.GetAxis("Mouse Y");*/
    }
    private void SetYRotation(float xAxis)
    {
        XAxis += xAxis* mouseSensitivity;
        playerTR.eulerAngles = new Vector3(0f, XAxis, 0f);
        /*XAxis += Input.GetAxis("Mouse X");
        YAxis -= Input.GetAxis("Mouse Y");*/
    }
    private void SetYPos(float yAxis)
    {
        camTR.localPosition = Vector3.zero-(GetCircle(yAxis/Mathf.Abs(yMinAxis))*3f);
    }
    public Vector3 GetCircle(float a)
    {
        return new Vector3(0, Mathf.Tan(-a/2f) , Mathf.Cos(a));
    }
    public void InfoRay()
    {
        Ray ray = new Ray(camTR.position, camTR.forward);
        if (Physics.Raycast(ray,40f,1<<6))
        {

        }
    }
}
