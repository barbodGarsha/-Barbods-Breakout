using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum status 
    {
        PLAYING,
        PAUSE,
        WON,
        GAMEOVER
    }

    public static GameManager instance = null;
    public GameObject gameover_screen;
    public GameObject gameover_score;
    public GameObject gameover_main_text;

    
    public Text score_text;
    public Text lives_text;

    public status game_status = status.PLAYING;
    public int bricks;
    public int lives = 1; 
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        lives_text.text = "LIVES: " + lives;
    }

    public void brick_destroyed() 
    {
        bricks--;
        score += 25;
        score_text.text = "SCORE: " + score;
        if (bricks == 0)
        {
            won();
        }
    }


    public void ball_hit_floor()
    {
        lives--;
        if (lives == 0)
        {
            lose();
        }
        else
        {
            lives_text.text = "LIVES: " + lives;
        }
    }

    
    void lose()
    {
        gameover_screen.SetActive(true);
        gameover_main_text.SetActive(true);
        gameover_score.SetActive(true);
        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
        gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Lost");
        game_status = status.GAMEOVER;
    }

    void won() 
    {
        gameover_screen.SetActive(true);
        gameover_main_text.SetActive(true);
        gameover_score.SetActive(true);
        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
        gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Won");
        game_status = status.WON;
    }

    void reset() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        //in the future we could add features like next level, go back to menu and ...
        if (game_status == status.GAMEOVER)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                reset();
            }
        }
        else if (game_status == status.WON)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                reset();
            }
        }
        else if (game_status == status.PAUSE)
        {

        }
    }
}
