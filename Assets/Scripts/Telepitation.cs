using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepitation : MonoBehaviour {

    public float maxTeleportDistance = 15.0f;

    public float laserThickness = 0.01f;

    private GameObject laser;
    private bool pressed = false;

    // Use this for initialization
    void Start()
    {
        laser = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        laser.transform.parent = this.transform;
        laser.transform.localScale = Vector3.zero;
        laser.transform.localPosition = Vector3.zero;
        laser.GetComponent<Renderer>().material.color = Color.red;
        laser.GetComponent<CapsuleCollider>().enabled = false;
        laser.transform.Rotate(new Vector3(0, 90, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward * maxTeleportDistance);
        RaycastHit hitInfo;
        //Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * maxTeleportDistance, Color.red, 0f, false);

        bool hit = Physics.Raycast(ray, out hitInfo, maxTeleportDistance);
        float distance = hit ? hitInfo.distance : maxTeleportDistance;

        if (OVRInput.GetDown(OVRInput.Button.Four))//buttoln.four press 3
        {
            laser.transform.localScale = new Vector3(laserThickness, laserThickness, distance);
            laser.transform.localPosition = new Vector3(0f, 0f, distance / 2f);
            

            pressed = true;

        }
        else if (OVRInput.GetUp(OVRInput.Button.Four))
        {
            laser.transform.localScale = Vector3.zero;
            laser.transform.localPosition = Vector3.zero;

            if (pressed)
            {

                if (hit && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("teleportable") && (hitInfo.point.x > 0 || !GameControlla.playState))
                {
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                }
                pressed = false;
            }
        }
    }
}
