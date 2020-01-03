using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballin : MonoBehaviour
{
    public enum ballState
    {
        inPlay,
        held,
        thrown
    };

    public ballState state;
    public int throwMultiplier = 5;
    public bool multY;

    // Use this for initialization
    void Start()
    {
        state = ballState.inPlay;
    }

    // Update is called once per frame
    void Update()
    {
        //check for collisions

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (state == ballState.inPlay)
        {
            Enemy e = obj.GetComponent<Enemy>();
            if (e != null && !e.hasBall)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = e.transform;
                transform.localPosition = new Vector3(0, 1.5f, .5f);
                e.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
                e.hasBall = true;
                state = ballState.held;
                //set a random time to wait
                e.timeToWait = Random.Range(.1f, 3.7f);
                e.timeWaited = 0;
                //Vector3 vec = new Vector3(Random.Range(-15.0f, 0.0f), 1, Random.Range(-15.0f, 15.0f));
                //e.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = ;
            }

        }
        if (obj == GameObject.FindGameObjectWithTag("floor"))
        {
            state = ballState.inPlay;
        }
        if (state == ballState.thrown)
        {
            Enemy e = obj.GetComponent<Enemy>();
            if (e != null)
            {
                e.enemyHealth -= 1;
                Debug.Log("hit enemy");
            }
            else if (obj == GameObject.FindGameObjectWithTag("Player"))
            {
                Playa p = GameObject.FindGameObjectWithTag("Player").GetComponent<Playa>();
                p.playaHealth -= 1;
                Debug.Log("hit player");
            }
        }
    }
    public void OnPickup()
    {
        state = ballState.held;
    }

    public void OnDrop()
    {
        state = ballState.thrown;
        Rigidbody p = GetComponent<Rigidbody>();
        //p.AddForce(new Vector3(transform.forward.x*8,0,transform.forward.z*8),ForceMode.VelocityChange);

        if (multY)
        {
            p.AddForce(new Vector3(p.velocity.x * throwMultiplier, p.velocity.y * throwMultiplier, p.velocity.z * throwMultiplier), ForceMode.VelocityChange);
        }
        else
        {
            p.AddForce(new Vector3(p.velocity.x * throwMultiplier, 0, p.velocity.z * throwMultiplier), ForceMode.VelocityChange);
        }
    }
}


