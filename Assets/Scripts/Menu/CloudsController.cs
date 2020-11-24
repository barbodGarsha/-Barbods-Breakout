using UnityEngine;

public class CloudsController : MonoBehaviour
{
    CloudsModel model;
    // Start is called before the first frame update
    void Start()
    {
        model = gameObject.GetComponent<CloudsModel>();
        model.start_pos = transform.position;
        model.pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        model.pos += new Vector3(CloudsModel.CLOUD_SPEED * Time.deltaTime, 0, 0);
        if (model.pos.x >= 15)
        {
            model.pos = model.start_pos;
        }
    }
}
