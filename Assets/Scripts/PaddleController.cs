using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    GameData model;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.instance.game_status == GameData.status.PLAYING)
        {
            Vector3 mouse_pos = Input.mousePosition;
            // Get distance the paddle is in front of the camera
            mouse_pos.z = Mathf.Abs(GameData.instance.paddle_model.pos.z - Camera.main.transform.position.z);
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

            //TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            GameData.instance.paddle_model.pos = new Vector2(Mathf.Clamp(mouse_pos.x, PaddleModel.LEFT_MAX, PaddleModel.RIGHT_MAX), GameData.instance.paddle_model.pos.y);

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
        GameData.instance.paddle_model.pos = new Vector3(0, -5.95f, 0);
    } 
}
