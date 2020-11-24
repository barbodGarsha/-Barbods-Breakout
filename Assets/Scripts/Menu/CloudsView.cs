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
        transform.position = model.pos;
    }
}
