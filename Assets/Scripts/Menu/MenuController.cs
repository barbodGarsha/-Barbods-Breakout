using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void play() 
    {
        MySceneManager.load_next_scene();
    }
}
