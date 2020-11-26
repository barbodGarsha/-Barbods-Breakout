using UnityEngine;

public class CloudsView : MonoBehaviour
{
    CloudsModel model;
    void Start()
    {
        model = gameObject.GetComponent<CloudsModel>();
    }
    void Update()
    {
        for (int i = 0; i < model.pos.Length; i++)
        {
            transform.GetChild(i).position = model.pos[i];
        }
        
    }
}
