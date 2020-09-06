using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Animator gunHolderAnimator;

    RaycastHit hit;
    Camera myCam;

    private void Start()
    {
        myCam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit))
        {
            if (Input.GetMouseButton(0))
            {
                if (hit.collider.GetComponent<Rigidbody>() != null)
                    hit.collider.GetComponent<Rigidbody>().AddForce(myCam.transform.forward * 10, ForceMode.Impulse);
            }

            SwitchScope();
        }
    }

    float lerpTime=0;
    void SwitchScope()
    {
        if (Input.GetMouseButton(1))
        {
            gunHolderAnimator.SetBool("isScope", true);
            myCam.fieldOfView = Mathf.Lerp(60,40,lerpTime);
            if (lerpTime < 1)
                lerpTime += Time.deltaTime*10;
        }
        else
        {
            gunHolderAnimator.SetBool("isScope", false);
            myCam.fieldOfView = Mathf.Lerp(60,40,lerpTime);
            if (lerpTime >0)
                lerpTime -= Time.deltaTime * 10;
        }
    }
}
