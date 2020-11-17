using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int bricks;
    public int lives = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void brick_destroyed() 
    {
        bricks--;
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
            //reset the ball not the game
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
