using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{

    public Sprite sprite_red, sprite_blue, sprite_unbreakable;
    public Sprite sprite_red_broke, sprite_blue_broke;

    public GameObject bricks;

    public GameObject Extra_live_prefab, short_paddle_prefab, long_paddle_prefab, fast_ball_prefab, slow_ball_prefab;

    private static GameConroller _instance;
    public static GameConroller instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject paddle;
    public GameObject ball;
    int lvl_index;
    GameModel game_model;
    BallModel ball_model;
    PaddleModel paddle_model;
    Ui ui_model;

    //BRICKS
    BricksModel[,] brick_model;

    int[,] map = new int[12, 14];


    string readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        string inp_ln = "";
        while (!inp_stm.EndOfStream)
        {
            inp_ln += inp_stm.ReadLine(); 
        }

        inp_stm.Close();
        return inp_ln;
    }
    int[,] make_lvl(string map)
    {
        int count = 0;
        int i = 0, j = 0;
        int[,] lvl = new int[12, 14];
        while (i < 12 && j < 14)
        {
            while (true)
            {
                if (map[count] >= '0' && map[count] <= '9') { break; }
                count++;
            }
            lvl[i, j] = int.Parse(map[count].ToString());
            j++;
            count++;
            if (map[count] == 'X')
            {
                j = 0;
                i++;
            }
        }
        return lvl;
    }
    //BRICKS
    void bricks_init(int[,] level)
    {
        
        int brick_counter = 0;
        game_model.bricks = 168;

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                brick_model[i, j] = new BricksModel();
                brick_model[i, j].g = bricks.transform.GetChild(i).GetChild(j).gameObject;
                switch (level[i, j])
                {
                    case 0://Unactive Brick
                        brick_model[i, j].type = BricksModel.BricksType.UNACTIVE;
                        bricks.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                        game_model.bricks--;
                        break;
                    case 1://Blue Brick
                        brick_model[i, j].type = BricksModel.BricksType.BLUE;
                        brick_model[i, j].sprite = sprite_blue;
                        brick_model[i, j].lives = 1; //For Now
                        break;
                    case 2://Red Brick
                        brick_model[i, j].type = BricksModel.BricksType.RED;
                        brick_model[i, j].sprite = sprite_red;
                        brick_model[i, j].lives = 2; //For Now
                        break;
                    case 3://Unbreakable Brick
                        brick_model[i, j].type = BricksModel.BricksType.UNBREAKABLE;
                        brick_model[i, j].sprite = sprite_unbreakable;
                        game_model.bricks--;
                        break;
                    default:
                        break;
                }
                bricks.transform.GetChild(i).GetChild(j).name = "Brick" + brick_counter;
                brick_model[i, j].name = "Brick" + brick_counter;
                brick_counter++;
            }
        }

        int pickup_index_x, pickup_index_y;
        for (int i = 0; i < 6; i++)
        {
            do
            {
                pickup_index_x = Random.Range(0, 12);
                pickup_index_y = Random.Range(0, 14);
            } while ((brick_model[pickup_index_x, pickup_index_y].type == BricksModel.BricksType.UNBREAKABLE) || (brick_model[pickup_index_x, pickup_index_y].type == BricksModel.BricksType.UNACTIVE));
            brick_model[pickup_index_x, pickup_index_y].have_pickup = true;
        }

    }

    public GameModel.Pickup make_pickup() 
    {
        GameModel.Pickup p = GameModel.Pickup.EXTRA_LIVE;
        int pickup_type;
        pickup_type = Random.Range(0, 3);
        switch (pickup_type)
        {
            case 0:
                switch (ball_model.speed_mode)
                {
                    case BallModel.SpeedMode.FAST:
                        p = GameModel.Pickup.SLOW_BALL;
                        break;
                    case BallModel.SpeedMode.NORMAL:
                        if (Random.Range(0, 2) == 1)
                        {
                            p = GameModel.Pickup.SLOW_BALL;
                        }
                        else
                        {
                            p = GameModel.Pickup.FAST_BALL;
                        }
                        break;
                    case BallModel.SpeedMode.SLOW:
                        p = GameModel.Pickup.FAST_BALL;
                        break;
                    default:
                        break;
                }
                break;
            case 1:
                switch (paddle_model.size)
                {
                    case PaddleModel.Size.SHORT:
                        p = GameModel.Pickup.LONG_PADDLE;
                        break;
                    case PaddleModel.Size.NORMAL:
                        if (Random.Range(0, 2) == 1)
                        {
                            p = GameModel.Pickup.LONG_PADDLE;
                        }
                        else
                        {
                            p = GameModel.Pickup.SHORT_PADDLE;
                        }
                        break;
                    case PaddleModel.Size.LONG:
                        p = GameModel.Pickup.SHORT_PADDLE;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                if (game_model.lives <= 2)
                {
                    p = GameModel.Pickup.EXTRA_LIVE;
                }
                else
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        switch (ball_model.speed_mode)
                        {
                            case BallModel.SpeedMode.FAST:
                                p = GameModel.Pickup.SLOW_BALL;
                                break;
                            case BallModel.SpeedMode.NORMAL:
                                if (Random.Range(0, 2) == 1)
                                {
                                    p = GameModel.Pickup.SLOW_BALL;
                                }
                                else
                                {
                                    p = GameModel.Pickup.FAST_BALL;
                                }
                                break;
                            case BallModel.SpeedMode.SLOW:
                                p = GameModel.Pickup.FAST_BALL;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (paddle_model.size)
                        {
                            case PaddleModel.Size.SHORT:
                                p = GameModel.Pickup.LONG_PADDLE;
                                break;
                            case PaddleModel.Size.NORMAL:
                                if (Random.Range(0, 2) == 1)
                                {
                                    p = GameModel.Pickup.LONG_PADDLE;
                                }
                                else
                                {
                                    p = GameModel.Pickup.SHORT_PADDLE;
                                }
                                break;
                            case PaddleModel.Size.LONG:
                                p = GameModel.Pickup.SHORT_PADDLE;
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            default:
                break;
        }
        return p;
    }
    public void pickup(string name) 
    {
        switch (name)
        {
            case "Extra Live":
                game_model.lives++;
                ui_model.ui_update |= Ui.UiUpdate.LIVES;
                break;
            case "Short Paddle":
                if (paddle_model.size != PaddleModel.Size.SHORT)
                {
                    paddle.transform.localScale -= new Vector3(0.24f, 0f, 0f);
                    if (paddle_model.size == PaddleModel.Size.NORMAL)
                    {
                        paddle_model.size = PaddleModel.Size.SHORT;
                    }
                    else
                    {
                        paddle_model.size = PaddleModel.Size.NORMAL;
                    }
                }
                break;
            case "Long Paddle":
                if (paddle_model.size != PaddleModel.Size.LONG)
                {
                    paddle.transform.localScale += new Vector3(0.240f, 0f, 0f);
                    if (paddle_model.size == PaddleModel.Size.NORMAL)
                    {
                        paddle_model.size = PaddleModel.Size.LONG;
                    }
                    else
                    {
                        paddle_model.size = PaddleModel.Size.NORMAL;
                    }
                }
                break;
            case "Fast Ball":
                if (ball_model.speed_mode != BallModel.SpeedMode.FAST)
                {
                    ball_model.speed += 2.5f;
                    if (ball_model.speed_mode == BallModel.SpeedMode.NORMAL)
                    {
                        ball_model.speed_mode = BallModel.SpeedMode.FAST;
                    }
                    else
                    {
                        ball_model.speed_mode = BallModel.SpeedMode.SLOW;
                    }
                }
                break;
            case "Slow Ball":
                if (ball_model.speed_mode != BallModel.SpeedMode.SLOW)
                {
                    ball_model.speed -= 2.5f;
                    if (ball_model.speed_mode == BallModel.SpeedMode.NORMAL)
                    {
                        ball_model.speed_mode = BallModel.SpeedMode.SLOW;
                    }
                    else
                    {
                        ball_model.speed_mode = BallModel.SpeedMode.NORMAL;
                    }
                }
                break;
            default:
                break;
        }
    }
    void Start()
    {
        lvl_index = PlayerPrefs.GetInt("Level Index");
        PlayerPrefs.SetInt("Level Index", 1);
        var data = GameData.instance;
        game_model = data.game_model;
        ball_model = data.ball_model;
        paddle_model = data.paddle_model;
        ui_model = data.ui_model;
        game_model.creative_mode = PlayerPrefs.GetInt("Creative Mode");
        //BRICKS
        data.brick_model = new BricksModel[12, 14];
        brick_model = data.brick_model;
        if (game_model.creative_mode == 1)
        {
            string lvl_map = readTextFile(Directory.GetCurrentDirectory() + @"\LevelMaker\CreativeModeLvl.txt");
            bricks_init(make_lvl(lvl_map));
        }
        else
        {
            
            string lvl_map = readTextFile(Directory.GetCurrentDirectory() + @"\LevelMaker\lvl" + lvl_index + ".txt");
            
            bricks_init(make_lvl(lvl_map));
        }

        ball_model.pos = ball.transform.position;
        paddle_model.pos = paddle.transform.position;

        GameData.instance.high_score = PlayerPrefs.GetInt("High Score" + lvl_index);
    }

    void Update()
    {
       
        switch (game_model.game_status)
        {
            case GameModel.status.PLAYING:
                if (Input.GetMouseButtonDown(0))
                {
                    ball_model.is_simulation_on = true;
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    game_model.game_status = GameModel.status.PAUSE;
                    ui_model.ui_update |= Ui.UiUpdate.SCREEN;
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (game_model.creative_mode == 0)
                    {
                        PlayerPrefs.SetInt("Creative Mode", 1);
                        reset();
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Creative Mode", 0);
                        reset();
                    }
                }
                break;
            case GameModel.status.PAUSE:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    game_model.game_status = GameModel.status.PLAYING;
                    ui_model.ui_update |= Ui.UiUpdate.SCREEN;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            case GameModel.status.WON:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    MySceneManager.load_menu_scene();
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    lvl_index++;
                    PlayerPrefs.SetInt("Level Index", lvl_index);
                }
                break;
            case GameModel.status.GAMEOVER:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    reset();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    MySceneManager.load_menu_scene();
                }
                break;
            default:
                break;
        }
    }


    void brick_destroyed()
    {
        game_model.bricks--;
        game_model.score += 25;
        if (GameData.instance.high_score < game_model.score)
        {
            GameData.instance.high_score = game_model.score;
            PlayerPrefs.SetInt("High Score" + lvl_index, GameData.instance.high_score);
            ui_model.ui_update |= Ui.UiUpdate.HIGHSCORE;
        }
        ui_model.ui_update |= Ui.UiUpdate.SCORE;

        if (game_model.bricks == 0)
        {
            won();
        }
    }


    void lose()
    {      
        game_model.game_status = GameModel.status.GAMEOVER;
        ui_model.ui_update |= Ui.UiUpdate.SCREEN;
    }

    void won()
    {
        game_model.game_status = GameModel.status.WON;
        ui_model.ui_update |= Ui.UiUpdate.SCREEN;
    }

    void reset()
    {
        MySceneManager.reset_scene();
    }


    void ball_hit_floor()
    {
        game_model.lives--;
        ui_model.ui_update |= Ui.UiUpdate.LIVES;
        if (game_model.lives == 0)
        {
            lose();
        }
        BallController.instance.reset_ball();
    }


    void ball_hit_brick(Collision2D collision) 
    {
        foreach (var brick in brick_model)
        {
            if (brick.name == collision.gameObject.name)
            {
                brick.lives--;
                switch (brick.type)
                {
                    case BricksModel.BricksType.RED:
                        brick.sprite = sprite_red_broke;
                        break;
                    case BricksModel.BricksType.BLUE:
                        break;
                    case BricksModel.BricksType.UNBREAKABLE:
                        BallController.instance.hit(collision);
                        SoundEffects.instance.ball_hit();
                        return;
                    default:
                        break;
                }
                brick.is_changed = true;
                if (brick.lives == 0)
                {
                    if (brick.have_pickup)
                    {
                        //drop pickup
                        GameModel.Pickup pickup = make_pickup();
                        GameObject temp;
                        switch (pickup)
                        {
                            case GameModel.Pickup.EXTRA_LIVE:
                                temp = Instantiate(Extra_live_prefab, new Vector3(brick.g.transform.position.x, brick.g.transform.position.y, brick.g.transform.position.z - 1), Quaternion.identity);
                                temp.name = "Extra Live";
                                break;
                            case GameModel.Pickup.SHORT_PADDLE:
                                temp = Instantiate(short_paddle_prefab, new Vector3(brick.g.transform.position.x, brick.g.transform.position.y, brick.g.transform.position.z - 1), Quaternion.identity);
                                temp.name = "Short Paddle";
                                break;
                            case GameModel.Pickup.LONG_PADDLE:
                                temp = Instantiate(long_paddle_prefab, new Vector3(brick.g.transform.position.x, brick.g.transform.position.y, brick.g.transform.position.z - 1), Quaternion.identity);
                                temp.name = "Long Paddle";
                                break;
                            case GameModel.Pickup.SLOW_BALL:
                                temp = Instantiate(slow_ball_prefab, new Vector3(brick.g.transform.position.x, brick.g.transform.position.y, brick.g.transform.position.z - 1), Quaternion.identity);
                                temp.name = "Slow Ball";
                                break;
                            case GameModel.Pickup.FAST_BALL:
                                temp = Instantiate(fast_ball_prefab, new Vector3(brick.g.transform.position.x, brick.g.transform.position.y, brick.g.transform.position.z - 1), Quaternion.identity);
                                temp.name = "Fast Ball";
                                break;
                            default:
                                break;
                        }
                    }          
                    brick_destroyed();
                }
                break;
            }
        }
        SoundEffects.instance.brick_destroyed();
        BallController.instance.hit(collision);
    }
    public void ball_hit(Collision2D collision) 
    {
        switch (collision.gameObject.tag)
        {
            case "Brick":
                ball_hit_brick(collision);
                break;
            case "Floor":
                ball_hit_floor();
                return;
            default:
                SoundEffects.instance.ball_hit();
                BallController.instance.hit(collision);
                break;
        }

    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Creative Mode", 0);
    }
}
