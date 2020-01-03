using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {
    public Transform target;
    public float rotateSpeed = 5.0f;

    private Vector3 rotatePoint;
	// Use this for initialization
	void Start () {
        rotatePoint = target.transform.position;
        transform.LookAt(target);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(target);
        transform.RotateAround(rotatePoint, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed);
    }
}
