using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	const float LEFT_MAX = -8f;
	const float RIGHT_MAX = 8f;

	public float paddle_speed = 35;
	private Vector2 paddle_pos = new Vector2(0, -4.5f);

	// Update is called once per frame
	void Update()
	{
		//We can move the paddle with arrow keys now
		//TODO: Add moving with mouse... it's hard to play with the Arrow keys
		float x_pos = transform.position.x + (Input.GetAxis("Horizontal") * paddle_speed * Time.deltaTime);

		//TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
		paddle_pos = new Vector2(Mathf.Clamp(x_pos, LEFT_MAX, RIGHT_MAX), paddle_pos.y);

		//Move the paddle
		transform.position = paddle_pos;
	}
}
