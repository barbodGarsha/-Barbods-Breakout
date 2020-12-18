﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui 
{
    [Flags]
    public enum UiUpdate
    {
        NONE = 0,
        LIVES = 1 << 0,
        SCORE = 1 << 1,
        SCREEN = 1 << 2,
        HIGHSCORE = 1 << 3,
        LEVEL = 1 << 4,
        ALL = LIVES | SCORE | SCREEN | HIGHSCORE | LEVEL
    }

    public UiUpdate ui_update = UiUpdate.ALL;
}
public class BallModel
{

    public const float OFFSET = 0.377f;
    public float speed = 10f;

    public enum SpeedMode
    {
        FAST,
        NORMAL,
        SLOW
    }
    public SpeedMode speed_mode = SpeedMode.NORMAL;
    public Vector3 pos;
    public Vector2 direction = new Vector2(0, 1);

    public bool is_simulation_on = false;
    public bool ball_hit = false;
    public Collision2D col;
}

public class PaddleModel
{
    //Just in case we added playing with keyboard
    public const float SPEED = 20f;

    public float left_max = -20f;
    public float right_max = 20f;

    public bool hit_wall = false;

    public enum Size 
    {
        SHORT,
        NORMAL,
        LONG
    }
    public Size size = Size.NORMAL;
    public Vector3 pos = new Vector3(0, 0, 0);

}

public class GameModel
{
    public enum Pickup
    {
        NONE,
        EXTRA_LIVE,
        SHORT_PADDLE,
        LONG_PADDLE,
        FAST_BALL,
        SLOW_BALL
    }

    public enum status
    {
        PLAYING,
        PAUSE,
        WON,
        GAMEOVER
    }

    public status game_status = status.PLAYING;
    public int bricks;
    public int lives = 3;
    public int score = 0;

    public int creative_mode = 0;

}

public class BricksModel 
{
   
    public enum BricksType
    {
        RED,
        BLUE,
        UNBREAKABLE,
        UNACTIVE
    }
    public string name;
    public GameObject g;
    public Sprite sprite;
    public BricksType type = BricksType.BLUE;
    public bool have_pickup = false;
    public int lives = 1;
    public bool is_changed = true;
    

}

public class GameData : MonoBehaviour
{
    private static GameData _instance;

    public static GameData instance { get { return _instance; } }

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
    public int lvl_index;

    public int high_score = 0;

    public BallModel ball_model = new BallModel();

    public PaddleModel paddle_model = new PaddleModel();

    public GameModel game_model = new GameModel();

    public Ui ui_model = new Ui();

    public BricksModel[,] brick_model = new BricksModel[12, 14];
}