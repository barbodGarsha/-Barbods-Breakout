using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleView : MonoBehaviour
{

    PaddleModel paddle_model;
    void Start()
    {
        var data = GameData.instance;
        paddle_model = data.paddle_model;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = paddle_model.pos;
    }
}
