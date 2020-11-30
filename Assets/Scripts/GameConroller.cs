using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{

    private static GameConroller _instance;
    public static GameConroller instance { get { return _instance; } }
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

    public GameObject paddle;
    public GameObject ball;
    // Start is called before the first frame update

    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;
    Ui ui_model;


    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;
        ui_model = data.ui_model;

        ball_model.pos = ball.transform.position;
        paddle_model.pos = paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void brick_destroyed()
    {
        game_model.bricks--;
        game_model.score += 25;
        ui_model.ui_update |= Ui.UiUpdate.SCORE;
        //if (bricks == 0)
        //{
        //    won();
        //}
    }


    void ball_hit_floor()
    {
        game_model.lives--;
        ui_model.ui_update |= Ui.UiUpdate.LIVES;
        if (game_model.lives == 0)
        {
            lose();
        }
    }


    void lose()
    {      
        game_model.game_status = GameModel.status.GAMEOVER;
        ui_model.ui_update |= Ui.UiUpdate.GAMEOVER_SCREEN;
    }

    //void won()
    //{
    //    gameover_screen.SetActive(true);
    //    gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
    //    gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Won");
    //    game_status = status.WON;
    //}

    void reset()
    {
        MySceneManager.reset_scene();
    }

    public void ball_hit(Collision2D collision) 
    {
        switch (collision.gameObject.tag)
        {
            case "Brick":
                Destroy(collision.gameObject);
                brick_destroyed();
                break;
            case "Player":
                break;
            case "Floor":
                ball_hit_floor();
                break;
            default:
                break;
        }

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

    //Turns Vector2 to a Vector3
    Vector3 to_vector3(Vector2 v2)
    {
        Vector3 v3 = new Vector3(v2.x, v2.y, 0f);
        return v3;
    }
}
