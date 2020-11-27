using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleView : MonoBehaviour
{

    GameData model;

    // Update is called once per frame
    void Update()
    {
        transform.position = GameData.instance.paddle_model.pos;
    }
}
