using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_MoveCube : MonoBehaviour
{
    public JoystickController joystick;
    public TouchField touchField;
    public Transform cam;


    public float speed;
    public float sprintSpeed;
    public float camSensitivity;

    public Text sens, spd;

    float playerSpeed;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(joystick.x, 0, joystick.y)*Time.deltaTime*playerSpeed;
        moveDirection = transform.TransformDirection(moveDirection);

        rb.MovePosition(transform.position+moveDirection);

        cam.localRotation = Quaternion.Euler(new Vector3(touchField.touchRotation.y-15,cam.localRotation.y, 0)* camSensitivity*-1);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, touchField.touchRotation.x* camSensitivity, transform.rotation.z));


        if (joystick.isSprinting)
            playerSpeed = sprintSpeed;
        else
            playerSpeed = speed;
    }

    public void CamSensitivity(float i)
    {
        camSensitivity = i;
        sens.text = i.ToString();
    }
    public void Speed(float i)
    {
        speed = i;
        spd.text = i.ToString();

    }
}
