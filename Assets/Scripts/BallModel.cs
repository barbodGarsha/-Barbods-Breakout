using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModel : MonoBehaviour
{

    public const float OFFSET = 0.377f;
    public const float SPEED = 20f;

    public Vector3 pos;
    public Vector2 direction;

    public bool is_simulation_on;
}
