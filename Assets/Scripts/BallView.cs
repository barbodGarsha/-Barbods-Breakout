﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    GameData model;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameData.instance.ball_model.pos;
    }
}
