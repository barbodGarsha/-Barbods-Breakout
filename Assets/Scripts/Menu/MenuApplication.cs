using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuApplication : MonoBehaviour
{
    public GameObject clouds_objects;
    void clouds_init()
    {
        clouds_objects.AddComponent<CloudsModel>();
        clouds_objects.AddComponent<CloudsController>();
        clouds_objects.AddComponent<CloudsView>();
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
