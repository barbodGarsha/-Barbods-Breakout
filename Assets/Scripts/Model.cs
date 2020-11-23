using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    //Game 
    public enum status
    {
        PLAYING,
        PAUSE,
        WON,
        GAMEOVER,
        MENU
    }

    public status game_status = status.PLAYING;
    public int bricks;
    public int lives = 1;
    int score = 0;

    //Clouds
    public const float CLOUD_SPEED = 0.65f;
   // public Vector3 clouds_start_pos; ??
}
