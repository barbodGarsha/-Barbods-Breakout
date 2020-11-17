using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	//a simple Brick that gets destroyed by the ball
    void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
            //TODO: Score system
            //TODO: animations and sounds
        }
	}
}
