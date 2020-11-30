using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    public GameObject paddle;
    public GameObject ball;
    // Start is called before the first frame update

    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;

    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;

        ball_model.pos = ball.transform.position;
        paddle_model.pos = paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void brick_destroyed()
    //{
    //    bricks--;
    //    score += 25;
    //    score_text.text = "SCORE: " + score;
    //    if (bricks == 0)
    //    {
    //        won();
    //    }
    //}


    //public void ball_hit_floor()
    //{
    //    lives--;
    //    if (lives == 0)
    //    {
    //        lose();
    //    }
    //    else
    //    {
    //        lives_text.text = "LIVES: " + lives;
    //    }
    //}


    //void lose()
    //{
    //    gameover_screen.SetActive(true);
    //    gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
    //    gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Lost");
    //    game_status = status.GAMEOVER;
    //}

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
}
