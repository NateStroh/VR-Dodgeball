using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlla : MonoBehaviour {
    public GameObject ballPrefab;
    public static bool playState = true;
    public int ballNum = 6;
    public static GameObject[] ballz;
    public static int enemyCount = 3;

	// Use this for initialization
	void Start () {
        ballz = new GameObject[ballNum];
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < ballNum; i++) {
            GameObject ball = ballz[i];
            if (ball == null) {
                ball = Instantiate(ballPrefab) as GameObject;
                ball.transform.position = new Vector3(Random.Range(-14.0f, 14.0f), 3, Random.Range(-14.0f, 14.0f));
                ballz[i] = ball;
            }
            if (ball.transform.position.y < -1) {
                Destroy(ball);
            }
        }
        if (enemyCount <= 0 && playState) {
            gameOver();
        }
	}

    public static GameObject findNearBall(Vector3 vec) {
        GameObject nearestBall = null;
        foreach (GameObject ball in ballz) {
            if (ball.GetComponent<Ballin>().state == Ballin.ballState.inPlay && ball.transform.position.x < 0) {
                if (nearestBall == null) {
                    nearestBall = ball;
                }
                if (Vector3.Distance(vec, ball.transform.position) < Vector3.Distance(vec, nearestBall.transform.position)) {
                    nearestBall = ball;
                }
            }
        }
        return nearestBall;
    }

    public static void gameOver() {
        playState = false;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ads");
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        p.transform.position = new Vector3(0, 6f, 20);
        if (enemyCount <= 0){
            foreach (GameObject o in objs){
                if (o.name != "GameWin"){
                    Destroy(o);
                }
            }
        }
        else {
            foreach (GameObject o in objs) {
                if (o.name != "GameLoss"){
                    Destroy(o);
                }
            }
        }
    }
}
