using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    BallModel ball_model;
    void Start()
    {
        var data = GameData.instance;
        ball_model = data.ball_model;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = ball_model.pos;
    }

    
    void OnCollisionEnter2D(Collision2D collision) 
    {
        GameConroller.instance.ball_hit(collision);
    }
}
