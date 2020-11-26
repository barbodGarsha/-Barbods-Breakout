using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    public GameObject gamedata_gameobject;
    GameData model;
    // Start is called before the first frame update
    void Start()
    {
        model = gamedata_gameobject.GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = model.ball_model.pos;
    }
}
