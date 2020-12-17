using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BricksView : MonoBehaviour
{

    BricksModel[,] brick_model;
    GameModel game_model;
    SpriteRenderer[,] sprite_renderer;
    
    void Start()
    {
        var data = GameData.instance;
        game_model = data.game_model;
        brick_model = data.brick_model;

        sprite_renderer = new SpriteRenderer[12, 14];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                sprite_renderer[i, j] = gameObject.transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (brick_model[i, j].is_changed)
                {

                    brick_model[i, j].is_changed = false;
                    if (brick_model[i, j].lives == 0)
                    {
                        gameObject.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                        continue;
                    }
                    sprite_renderer[i, j].sprite = brick_model[i, j].sprite;
                }
            }
        }
    }
}
