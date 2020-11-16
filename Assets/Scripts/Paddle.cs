using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	public float paddle_speed = 0.5f;
	private Vector3 paddle_pos = new Vector3(0, -4.5f, 0);

	// Update is called once per frame
	void Update()
	{
		//We can move the paddle with arrow keys now
		//TODO: Add moving with mouse... it's hard to play with the Arrow keys
		float x_pos = transform.position.x + (Input.GetAxis("Horizontal") * paddle_speed);

		//TODO: after designing a level the max and min value in Mathf.Clamp() function needs to change
		paddle_pos = new Vector3(Mathf.Clamp(x_pos, -8f, 8f), paddle_pos.y, 0);

		//Move the paddle
		transform.position = paddle_pos;
	}
}
