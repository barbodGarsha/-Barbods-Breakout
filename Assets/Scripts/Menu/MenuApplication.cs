using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuApplication : MonoBehaviour
{
    public GameObject[] clouds_objects;
    void clouds_init() 
    {
        foreach (GameObject g in clouds_objects)
        {
            g.AddComponent<CloudsModel>();
            g.AddComponent<CloudsController>();
            g.AddComponent<CloudsView>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        clouds_init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
