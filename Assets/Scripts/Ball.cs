﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 direction = new Vector2(1, 1);
    public float speed = 20f;

    //calculates which side of the paddle the ball hits
    int hit_pos(Vector2 ball_pos, Vector2 paddle_pos, float paddle_width)
    {
        float pos = (ball_pos.x - paddle_pos.x) / paddle_width;

        //  pos:     -1 <-  0  -> 1
        //            .  .  .  .  .
        //  Paddle:  [/////////////]
        //
        if (pos > 0) //the ball hit the right side of the paddle
        {
            return 1;
        }
        else if (pos < 0) //the ball hit the left side of the paddle
        {
            return -1;
        }

        //the ball hit the center of the paddle
        return 0;
    }

    //Turns Vector2 to a Vector3
    Vector3 to_vector3(Vector2 v2)
    {
        Vector3 v3 = new Vector3(v2.x, v2.y, 0f);
        return v3;
    }

    void Start()
    {
        direction.Normalize();
    }

    void Update()
    {
        //The ball keeps moving till it hits something. then it changes it's direction
        this.transform.position += to_vector3(direction) * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normalVector;

        // in order to give the game a smooth and enjoable gameplay player needs to have more control over the ball
        // for example when we hit the ball with the right side of the paddle the ball goes to right
        // TODO: add more control over the ball
        if (collision.gameObject.tag == "Player")
        {
            //Where did the ball hit the paddle?
            int x = hit_pos(this.transform.position, collision.transform.position, collision.collider.bounds.size.x);
            
            //Right
            if (x == 1)
            {
                //then bounce the ball to the right

                if (direction.x >= -1 && direction.x <= 0)
                {
                    direction = this.transform.position - collision.transform.position;
                    direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    direction = Vector3.Reflect(direction, normalVector);
                    direction.Normalize();

                }
            }
            else // Left or right in the middle
            {
                //then bounce the ball to the left

                if (direction.x <= 1 && direction.x >= 0)
                {

                    direction = this.transform.position - collision.transform.position;
                    direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    direction = Vector3.Reflect(direction, normalVector);
                    direction.Normalize();

                }
            }

        }
        else // When the ball hits other objects in the game it should just bounce on it like always
        {
            normalVector = collision.contacts[0].normal;
            direction = Vector3.Reflect(direction, normalVector);
            direction.Normalize();  
        }


    }
}