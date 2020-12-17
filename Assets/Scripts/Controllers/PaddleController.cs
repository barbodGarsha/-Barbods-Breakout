using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;
    public GameObject paddle;

    private static PaddleController _instance;
    public static PaddleController instance { get { return _instance; } }
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

    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (game_model.game_status == GameModel.status.PLAYING)
        {
            Vector3 mouse_pos = Input.mousePosition;
            // Get distance the paddle is in front of the camera
            mouse_pos.z = Mathf.Abs(paddle_model.pos.z - Camera.main.transform.position.z);
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

            if (paddle_model.hit_wall)
            {
                if (paddle_model.pos.x > 0)
                {
                    paddle_model.right_max = paddle_model.pos.x;
                }
                else
                {
                    paddle_model.left_max = paddle_model.pos.x;
                }
            }
            //TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            paddle_model.pos = new Vector2(Mathf.Clamp(mouse_pos.x, paddle_model.left_max, paddle_model.right_max), paddle_model.pos.y);

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
        paddle.transform.localScale -= new Vector3(0.24f, 0f, 0f);
        paddle_model.size = PaddleModel.Size.NORMAL;
        paddle_model.pos = new Vector3(0, -5.95f, 0);
    } 
}
