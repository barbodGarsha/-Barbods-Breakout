﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    BallModel model;
    // Start is called before the first frame update
    void Start()
    {
        model = gameObject.GetComponent<BallModel>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = model.pos;
    }
}
