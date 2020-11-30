using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    public GameObject paddle;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        GameData.instance.ball_model.pos = ball.transform.position;
        GameData.instance.paddle_model.pos = paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
