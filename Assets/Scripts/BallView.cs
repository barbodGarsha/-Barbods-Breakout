using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameData.instance.ball_model.pos;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        BallController.instance.hit(collision);
    }
}
