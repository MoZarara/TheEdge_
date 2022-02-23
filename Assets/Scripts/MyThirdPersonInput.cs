using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThirdPersonInput : MonoBehaviour
{
    public FixedJoystick leftJoystick;
    //public FixedJoystick cameraJoystick;
    public FixedButton button;
    public FixedTouchField TouchField;
    protected Player_Movement control;


    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.2f;

    [Header("")]
    public Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Player_Movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.playerStop)
        {
            if (controller.handleJoystick.GetComponent<RectTransform>().localPosition.x > 0 
                || controller.handleJoystick.GetComponent<RectTransform>().localPosition.y > 0 
            || controller.handleJoystick.GetComponent<RectTransform>().localPosition.x < 0
                || controller.handleJoystick.GetComponent<RectTransform>().localPosition.y < 0)

            {
                control.m_Jump = button.Pressed;
                control.hori_Input = leftJoystick.Direction.x;
                control.ver_Input = leftJoystick.Direction.y;
            } else if (controller.handleJoystick.GetComponent<RectTransform>().localPosition.x == 0 && controller.handleJoystick.GetComponent<RectTransform>().localPosition.y == 0)
            {
                control.m_Jump = button.Pressed;
                control.hori_Input = 0;
                control.ver_Input = 0;

            }
            


            
            

            CameraAngle += TouchField.TouchDist.x * CameraAngleSpeed;
            //CameraAngle += cameraJoystick.Direction.x * CameraAngleSpeed;

            Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3/*(0, 3, 4)*/(0, 3.09f, -2.14f);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up */*2f*//*1*/ 1.9f - Camera.main.transform.position, Vector3.up);
        }
        
    }
}
