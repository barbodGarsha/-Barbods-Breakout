using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    GameData model;

    // Update is called once per frame
    void Update()
    {
        if (GameData.instance.game_status == GameData.status.PLAYING)
        {
            if (GameData.instance.ball_model.is_simulation_on)
            {
                //The ball keeps moving till it hits something. then it changes it's direction
                GameData.instance.ball_model.pos += to_vector3(GameData.instance.ball_model.direction) * GameData.BallModel.SPEED * Time.deltaTime;
            }
            else
            {
                GameData.instance.ball_model.pos = new Vector3(GameData.instance.paddle_model.pos.x, GameData.instance.paddle_model.pos.y + GameData.BallModel.OFFSET, 0);
            }
        }
    }

    public void reset_ball()
    {
        GameData.instance.ball_model.is_simulation_on = false;
        GameData.instance.ball_model.pos = new Vector3(GameData.instance.paddle_model.pos.x, GameData.instance.paddle_model.pos.y + GameData.BallModel.OFFSET, 0);
        GameData.instance.ball_model.direction = new Vector2(0, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameData.instance.ball_model.is_simulation_on) { return; }

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

                if (GameData.instance.ball_model.direction.x >= -1 && GameData.instance.ball_model.direction.x <= 0)
                {
                    GameData.instance.ball_model.direction = this.transform.position - collision.transform.position;
                    GameData.instance.ball_model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    GameData.instance.ball_model.direction = Vector3.Reflect(GameData.instance.ball_model.direction, normalVector);
                    GameData.instance.ball_model.direction.Normalize();

                }
            }
            else // Left or right in the middle
            {
                //then bounce the ball to the left

                if (GameData.instance.ball_model.direction.x <= 1 && GameData.instance.ball_model.direction.x >= 0)
                {

                    GameData.instance.ball_model.direction = this.transform.position - collision.transform.position;
                    GameData.instance.ball_model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    GameData.instance.ball_model.direction = Vector3.Reflect(GameData.instance.ball_model.direction, normalVector);
                    GameData.instance.ball_model.direction.Normalize();

                }
            }

        }
        else if (collision.gameObject.tag == "Floor")
        {
            //GameManager.instance.ball_hit_floor();
            //reset_ball();

        }
        else // When the ball hits other objects in the game it should just bounce on it like always
        {
            normalVector = collision.contacts[0].normal;
            GameData.instance.ball_model.direction = Vector3.Reflect(GameData.instance.ball_model.direction, normalVector);
            GameData.instance.ball_model.direction.Normalize();
        }


    }

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
}
