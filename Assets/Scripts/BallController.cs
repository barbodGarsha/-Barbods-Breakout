using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    BallModel model;
    
    // Start is called before the first frame update
    void Start()
    {
        model = gameObject.GetComponent<BallModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (model.is_simulation_on)
        {
            //The ball keeps moving till it hits something. then it changes it's direction
            model.pos += to_vector3(model.direction) * BallModel.SPEED * Time.deltaTime;
        }
        //lets do it in game controller
        //else
        //{
        //    model.pos = new Vector3(paddle.transform.position.x, paddle.transform.position.y + BallModel.OFFSET, 0);
        //}

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!model.is_simulation_on) { return; }

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

                if (model.direction.x >= -1 && model.direction.x <= 0)
                {
                    model.direction = this.transform.position - collision.transform.position;
                    model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    model.direction = Vector3.Reflect(model.direction, normalVector);
                    model.direction.Normalize();

                }
            }
            else // Left or right in the middle
            {
                //then bounce the ball to the left

                if (model.direction.x <= 1 && model.direction.x >= 0)
                {

                    model.direction = this.transform.position - collision.transform.position;
                    model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    model.direction = Vector3.Reflect(model.direction, normalVector);
                    model.direction.Normalize();

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
            model.direction = Vector3.Reflect(model.direction, normalVector);
            model.direction.Normalize();
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
