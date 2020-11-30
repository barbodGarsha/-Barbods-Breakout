using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModel
{

    public const float OFFSET = 0.377f;
    public const float SPEED = 10f;

    public Vector3 pos;
    public Vector2 direction = new Vector2(0, 1);

    public bool is_simulation_on = true;
    public bool ball_hit = false;
    public Collision2D col;
}

public class PaddleModel
{
    //Just in case we added playing with keyboard
    public const float SPEED = 20f;

    public const float LEFT_MAX = -10.06f;
    public const float RIGHT_MAX = 10.06f;

    public Vector3 pos = new Vector3(0, 0, 0);

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
    
    public enum status
    {
        PLAYING,
        PAUSE,
        WON,
        GAMEOVER,
        MENU
    }

    public status game_status = status.PLAYING;



    public BallModel ball_model = new BallModel();

    public PaddleModel paddle_model = new PaddleModel();

}
