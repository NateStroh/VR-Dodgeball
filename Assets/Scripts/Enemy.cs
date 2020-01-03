using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int enemyHealth = 3;
    public bool hasBall;
    public float timeToWait = 1.3f;
    public float timeWaited;
    public GameObject target;
	// Use this for start of our venture
	void Start () {
        hasBall = false;
	}
	
	// Update is called once per fortnight
	void Update () {
        if (hasBall)
        {
            transform.LookAt(target.transform);
            timeWaited += .01f;
            if (timeWaited > timeToWait)
            {
                throwDaBall();
            }
        }
        else {
            GameObject go = GameControlla.findNearBall(transform.position);
            if (go != null)
            {
                GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = go.transform;
            }
            else {
                GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
            }
        }

        if (enemyHealth <= 0) {
            GameControlla.enemyCount --;
            Destroy(this.gameObject);
        }
	}

    public void throwDaBall() {
        // removeth thy ball as child, sendth forth the ball in a forward manner, set thy states
        Ballin ball = gameObject.GetComponentInChildren<Ballin>();
        //Debug.Log(ball.name);
        ball.transform.parent = null;

        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.isKinematic = false;

        Vector3 throwDirec = transform.forward;
        throwDirec.y += Random.Range(.15f, .3f);
        ballBody.velocity = throwDirec * Random.Range(10.0f, 20.0f);

        hasBall = false;
        ball.state = Ballin.ballState.thrown;
        timeWaited = 0;
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
    }

}
