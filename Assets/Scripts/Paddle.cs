using UnityEngine;

public class Paddle : MonoBehaviour
{
    const float LEFT_MAX = -10.06f;
    const float RIGHT_MAX = 10.06f;

    //Note: this is only needed for the Moving with the keyboard
    //public float paddle_speed = 35;

    private Vector2 paddle_pos;
    private void Start()
    {
        paddle_pos = this.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.game_status == GameManager.status.PLAYING)
        {
            Vector3 mouse_pos = Input.mousePosition;
            // Get distance the paddle is in front of the camera
            mouse_pos.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

            //TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            paddle_pos = new Vector2(Mathf.Clamp(mouse_pos.x, LEFT_MAX, RIGHT_MAX), paddle_pos.y);

            //Move the paddle
            transform.position = paddle_pos;

            //----------------------------------------
            // Moving the paddle with kyboard 
            ////We can move the paddle with arrow keys now
            ////TODO: Add moving with mouse... it's hard to play with the Arrow keys
            //float x_pos = transform.position.x + (Input.GetAxis("Horizontal") * paddle_speed * Time.deltaTime);

            ////TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
            //paddle_pos = new Vector2(Mathf.Clamp(x_pos, LEFT_MAX, RIGHT_MAX), paddle_pos.y);

            ////Move the paddle
            //transform.position = paddle_pos;
            //----------------------------------------
        }

    }
}
