using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    const float CLOUD_SPEED = 1f;
    Vector3 start_pos;
    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(CLOUD_SPEED * Time.deltaTime, 0, 0);
        if (transform.position.x >= 15)
        {
            transform.position = start_pos;
        }
    }
}
