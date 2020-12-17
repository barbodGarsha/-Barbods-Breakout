using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{
    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;

    private static BallController _instance;
    public static BallController instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;

    }

    void FixedUpdate()
    {
        if (game_model.game_status == GameModel.status.PLAYING)
        {
            if (ball_model.is_simulation_on)
            {
                ball_model.pos += to_vector3(ball_model.direction) * ball_model.speed * Time.fixedDeltaTime;
            }
            else
            {
                ball_model.pos = new Vector3(paddle_model.pos.x, paddle_model.pos.y + BallModel.OFFSET, 0);
            }
        }
    }

    public void reset_ball()
    {
        ball_model.is_simulation_on = false;
        ball_model.speed = 10f;
        ball_model.speed_mode = BallModel.SpeedMode.NORMAL;
        ball_model.pos = new Vector3(paddle_model.pos.x, paddle_model.pos.y + BallModel.OFFSET, 0);
        ball_model.direction = new Vector2(0, 1);
    }

    public void hit(Collision2D collision)
    {
        Vector3 normalVector;


        // in order to give the game a smooth and enjoable gameplay player needs to have more control over the ball
        // for example when we hit the ball with the right side of the paddle the ball goes to right
        // TODO: add more control over the ball
        if (collision.gameObject.tag == "Player")
        {
            //Where did the ball hit the paddle?
            int x = hit_pos(ball_model.pos, collision.transform.position, collision.collider.bounds.size.x);

            //Right
            if (x == 1)
            {
                //then bounce the ball to the right

                if (ball_model.direction.x >= -1 && ball_model.direction.x <= 0)
                {
                    ball_model.direction = ball_model.pos - collision.transform.position;
                    ball_model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    ball_model.direction = Vector3.Reflect(ball_model.direction, normalVector);
                    ball_model.direction.Normalize();

                }
            }
            else // Left or right in the middle
            {
                //then bounce the ball to the left

                if (ball_model.direction.x <= 1 && ball_model.direction.x >= 0)
                {

                    ball_model.direction = ball_model.pos - collision.transform.position;
                    ball_model.direction.Normalize();
                }
                else
                {
                    normalVector = collision.contacts[0].normal;
                    ball_model.direction = Vector3.Reflect(ball_model.direction, normalVector);
                    ball_model.direction.Normalize();

                }
            }

        }
        else // When the ball hits other objects in the game it should just bounce on it like always
        {
            normalVector = collision.contacts[0].normal;
            ball_model.direction = Vector3.Reflect(ball_model.direction, normalVector);
            ball_model.direction.Normalize();
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

    Vector3 to_vector3(Vector2 v2)
    {
        Vector3 v3 = new Vector3(v2.x, v2.y, 0f);
        return v3;
    }
}
