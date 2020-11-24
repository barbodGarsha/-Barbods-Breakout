using UnityEngine;

public class GameModel : MonoBehaviour
{

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
    //public int lives = 1;
    //int score = 0;

}
