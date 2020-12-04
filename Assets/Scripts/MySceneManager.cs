using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static void load_next_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void load_pre_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public static void reset_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void load_menu_scene()
    {
        SceneManager.LoadScene(0);
    }


}
