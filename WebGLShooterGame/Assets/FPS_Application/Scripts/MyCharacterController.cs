using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public float jumpPower;

    Rigidbody rigidBody;
    Camera myCam;
    
    Vector3 inputs;
    bool isGrounded;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        myCam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        Moveplayer();
        RotateCamera();
    }

    void Moveplayer()
    {
        inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveDirection = transform.TransformDirection(inputs);
        rigidBody.MovePosition((transform.position + (moveDirection * (speed*100) *Time.deltaTime)));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                rigidBody.AddForce(Vector3.up*jumpPower*1000);
                isGrounded = false;
            }
        }
    }

    float rotY = 0;
    float rotX = 0;
    void RotateCamera()
    {
        rotY += Input.GetAxis("Mouse X") * (sensitivity*100) * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * (sensitivity*100) * Time.deltaTime;
        rotX = Mathf.Clamp(rotX,-30,60);
        
        
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, rotY, transform.rotation.z));

        myCam.transform.localRotation = Quaternion.Euler(new Vector3(-rotX,0,0));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            isGrounded = true;
    }


}
