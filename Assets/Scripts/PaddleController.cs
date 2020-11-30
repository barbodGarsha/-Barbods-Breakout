using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;

    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;
    }

    // Update is called once per frame
    void Update()
    {
        if (game_model.game_status == GameModel.status.PLAYING)
        {
            Vector3 mouse_pos = Input.mousePosition;
            // Get distance the paddle is in front of the camera
            mouse_pos.z = Mathf.Abs(paddle_model.pos.z - Camera.main.transform.position.z);
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

            //TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            paddle_model.pos = new Vector2(Mathf.Clamp(mouse_pos.x, PaddleModel.LEFT_MAX, PaddleModel.RIGHT_MAX), paddle_model.pos.y);

            //----------------------------------------
            // Moving the paddle with kyboard 
            ////We can move the paddle with arrow keys now
            ////TODO: Add moving with mouse... it's hard to play with the Arrow keys
            //float x_pos = transform.position.x + (Input.GetAxis("Horizontal") * paddle_speed * Time.deltaTime);

            ////TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            //paddle_pos = new Vector2(Mathf.Clamp(x_pos, LEFT_MAX, RIGHT_MAX), paddle_pos.y);
            //----------------------------------------
        }

    }

    public void paddle_pos_reset() 
    {
        paddle_model.pos = new Vector3(0, -5.95f, 0);
    } 
}
