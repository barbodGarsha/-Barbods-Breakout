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

    void Update()
    {
        switch (game_model.game_status)
        {
            case GameModel.status.PLAYING:
                if (Input.GetMouseButtonDown(0))
                {
                    ball_model.is_simulation_on = true;
                }
                break;
            case GameModel.status.PAUSE:
                break;
            case GameModel.status.WON:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            case GameModel.status.GAMEOVER:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            default:
                break;
        }
    }


    void brick_destroyed()
    {
        game_model.bricks--;
        game_model.score += 25;
        ui_model.ui_update |= Ui.UiUpdate.SCORE;
        if (game_model.bricks == 0)
        {
            won();
        }
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
        ui_model.ui_update |= Ui.UiUpdate.SCREEN;
    }

    void won()
    {
        game_model.game_status = GameModel.status.WON;
        ui_model.ui_update |= Ui.UiUpdate.SCREEN;
    }

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
            case "Floor":
                ball_hit_floor();
                BallController.instance.reset_ball();
                break;
            default:
                break;
        }
        BallController.instance.hit(collision);
    }

}
