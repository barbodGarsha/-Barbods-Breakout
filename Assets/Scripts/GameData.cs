using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public static GameData instance = null;

    void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
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

    public class BallModel
    {

        public const float OFFSET = 0.377f;
        public const float SPEED = 20f;

        public Vector3 pos;
        public Vector2 direction = new Vector2(0, 1);

        public bool is_simulation_on = true;
    }

    public BallModel ball_model;
    public class PaddleModel
    {
        //Just in case we added playing with keyboard
        public const float SPEED = 20f;

        public const float LEFT_MAX = -10.06f;
        public const float RIGHT_MAX = 10.06f;

        public Vector3 pos = new Vector3(0, 0, 0);

        public bool is_simulation_on;
    }

    public PaddleModel paddle_model;

}
