using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Text score_text;
    public Text lives_text;

    public int bricks;
    public int lives = 1; //WARNING : don't add lives the code is not finished yet
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        lives_text.text = "LIVES: " + lives;
    }

    public void brick_destroyed() 
    {
        bricks--;
        score += 25;
        score_text.text = "SCORE: " + score;
        if (bricks == 0)
        {
            Debug.Log("WON");
            //TODO: you won scene or something
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
            //NOT FINISHED
        }
    }

    
    void lose()
    {
        //TODO: gameover scene or something should be shown but for now it just resets the level
        reset();
    }

    void reset() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
