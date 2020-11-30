using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiView : MonoBehaviour
{
    public GameObject gameover_screen;
    public GameObject gameover_score;
    public GameObject gameover_main_text;

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

    // Update is called once per frame
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
            if ((ui_model.ui_update & Ui.UiUpdate.GAMEOVER_SCREEN) == Ui.UiUpdate.GAMEOVER_SCREEN)
            {
                switch (game_model.game_status)
                {
                    case GameModel.status.PLAYING:
                        gameover_screen.SetActive(false);
                        break;
                    case GameModel.status.PAUSE:
                        break;
                    case GameModel.status.WON:
                        gameover_screen.SetActive(true);
                        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + game_model.score);
                        gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Won");
                        break;
                    case GameModel.status.GAMEOVER:
                        gameover_screen.SetActive(true);
                        gameover_score.gameObject.GetComponent<TextMeshProUGUI>().SetText("Score: " + game_model.score);
                        gameover_main_text.gameObject.GetComponent<TextMeshProUGUI>().SetText("You Lost");
                        break;
                    default:
                        break;
                }

            }
            ui_model.ui_update = Ui.UiUpdate.NONE;
        }
    }
}
