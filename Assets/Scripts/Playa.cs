using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playa : MonoBehaviour {
    public int playaHealth = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //check for end game health<=0
        //switch to endcam
        if (playaHealth <= 0) {
            if (GameControlla.playState)
                GameControlla.gameOver();
        }
	}
}
