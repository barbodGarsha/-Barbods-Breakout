using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiView : MonoBehaviour
{
    public GameObject screen;
    public GameObject gameover_score;
    public GameObject main_text;
    
    public GameObject level_text;

    public Text score_text;
    public Text lives_text;

    Ui ui_model;
    GameModel game_model;

    void Start()
    {
        var data = GameData.instance;
        ui_model = data.ui_model;
        game_model = data.game_model;
    }

    void Update()
    {
        if ((ui_model.ui_update & Ui.UiUpdate.ALL) != 0)
        {
            if ((ui_model.ui_update & Ui.UiUpdate.LIVES) == Ui.UiUpdate.LIVES)
            {
                lives_text.text = "LIVES: " + game_model.lives;
            }
            if ((ui_model.ui_update & Ui.UiUpdate.SCORE) == Ui.UiUpdate.SCORE)
            {
                score_text.text = "SCORE: " + game_model.score;
            }
            if ((ui_model.ui_update & Ui.UiUpdate.LEVEL) == Ui.UiUpdate.LEVEL)
            {
                if (PlayerPrefs.GetInt("Level Won" + GameData.instance.lvl_index) == 1)
                {
                    level_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("LEVEL: " + GameData.instance.lvl_index + " (Completed)");
                }
                else
                {
                    level_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("LEVEL: " + GameData.instance.lvl_index);
                }
               
            }
            if ((ui_model.ui_update & Ui.UiUpdate.SCREEN) == Ui.UiUpdate.SCREEN)
            {

                switch (game_model.game_status)
                {
                    case GameModel.status.PLAYING:
                        screen.SetActive(false);
                        break;
                    case GameModel.status.PAUSE:
                        screen.SetActive(true);
                        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + game_model.score);
                        main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("Paused");
                        break;
                    case GameModel.status.WON:
                        screen.SetActive(true);
                        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Congrats");
                        main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Won");
                        break;
                    case GameModel.status.GAMEOVER:
                        screen.SetActive(true);
                        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + game_model.score);
                        main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Lost");
                        break;
                    default:
                        break;
                }

            }
            ui_model.ui_update = Ui.UiUpdate.NONE;
        }
    }
}
