using UnityEngine;

public class CloudsController : MonoBehaviour
{
    CloudsModel model;
    // Start is called before the first frame update
    void Start()
    {
        model = gameObject.GetComponent<CloudsModel>();
        int childern_count = transform.childCount;

        model.start_pos = new Vector3[childern_count];
        model.pos = new Vector3[childern_count];
        for (int i = 0; i < childern_count; ++i)
        {
            model.start_pos[i] = transform.GetChild(i).position;
            model.pos[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < model.pos.Length; i++)
        {
            model.pos[i] += new Vector3(CloudsModel.CLOUD_SPEED * Time.deltaTime, 0, 0);
            if (model.pos[i].x >= 15)
            {
                model.pos[i] = model.start_pos[i];
            }
        }
    }
}
