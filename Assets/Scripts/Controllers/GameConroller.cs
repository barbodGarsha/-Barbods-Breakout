﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{

    public Sprite sprite_red, sprite_blue, sprite_unbreakable;
    public Sprite sprite_red_broke, sprite_blue_broke;

    public GameObject bricks;

    public GameObject Extra_live_prefab;

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
    BricksModel[] brick_model;

    void bricks_init()
    {
        for (int i = 0; i < brick_model.Length; i++)
        {
            brick_model[i] = new BricksModel();
            brick_model[i].g = bricks.transform.GetChild(i).gameObject;
            switch (bricks.transform.GetChild(i).name)
            {
                case "Brick Blue":
                    brick_model[i].type = BricksModel.BricksType.BLUE;
                    brick_model[i].sprite = sprite_blue;
                    brick_model[i].lives = 1; //For Now
                    break;
                case "Brick Red":
                    brick_model[i].type = BricksModel.BricksType.RED;
                    brick_model[i].sprite = sprite_red;
                    brick_model[i].lives = 2; //For Now
                    break;
                case "Brick Unbreakable":
                    brick_model[i].type = BricksModel.BricksType.UNBREAKABLE;
                    brick_model[i].sprite = sprite_unbreakable;
                    game_model.bricks--;
                    break;
                default:
                    break;
            }
            bricks.transform.GetChild(i).name = "Brick" + i;
            brick_model[i].name = "Brick" + i;
        }

        int extra_live_index;
        do
        {
            extra_live_index = Random.Range(0, brick_model.Length);
        } while (brick_model[extra_live_index].type == BricksModel.BricksType.UNBREAKABLE);
        brick_model[extra_live_index].pickup = BricksModel.Pickup.EXTRA_LIVE;


    }

    public void pickup(string name) 
    {
        switch (name)
        {
            case "Extra Live":
                game_model.lives++;
                ui_model.ui_update |= Ui.UiUpdate.LIVES;
                break;
            default:
                break;
        }
    }
    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;
        ui_model = data.ui_model;

        game_model.bricks = bricks.transform.childCount;
        data.brick_model = new BricksModel[game_model.bricks];
        brick_model = data.brick_model;
        bricks_init();

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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    game_model.game_status = GameModel.status.PAUSE;
                    ui_model.ui_update |= Ui.UiUpdate.SCREEN;
                }
                break;
            case GameModel.status.PAUSE:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    game_model.game_status = GameModel.status.PLAYING;
                    ui_model.ui_update |= Ui.UiUpdate.SCREEN;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            case GameModel.status.WON:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            case GameModel.status.GAMEOVER:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
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


    void ball_hit_floor()
    {
        game_model.lives--;
        ui_model.ui_update |= Ui.UiUpdate.LIVES;
        if (game_model.lives == 0)
        {
            lose();
        }
        BallController.instance.reset_ball();
    }


    void ball_hit_brick(Collision2D collision) 
    {
        foreach (var brick in brick_model)
        {
            if (brick.name == collision.gameObject.name)
            {
                brick.lives--;
                switch (brick.type)
                {
                    case BricksModel.BricksType.RED:
                        brick.sprite = sprite_red_broke;
                        break;
                    case BricksModel.BricksType.BLUE:
                        break;
                    case BricksModel.BricksType.UNBREAKABLE:
                        BallController.instance.hit(collision);
                        SoundEffects.instance.ball_hit();
                        return;
                    default:
                        break;
                }
                brick.is_changed = true;
                if (brick.lives == 0)
                {
                    switch (brick.pickup)
                    {
                        case BricksModel.Pickup.EXTRA_LIVE:
                            var temp = Instantiate(Extra_live_prefab, brick.g.transform.position, Quaternion.identity);
                            temp.name = "Extra Live";
                            break;
                        default:
                            break;
                    }
                    brick_destroyed();
                }
                break;
            }
        }
        SoundEffects.instance.brick_destroyed();
        BallController.instance.hit(collision);
    }
    public void ball_hit(Collision2D collision) 
    {
        switch (collision.gameObject.tag)
        {
            case "Brick":
                ball_hit_brick(collision);
                break;
            case "Floor":
                ball_hit_floor();
                return;
            default:
                SoundEffects.instance.ball_hit();
                BallController.instance.hit(collision);
                break;
        }

    }

}
