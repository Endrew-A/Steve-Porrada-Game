using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public GameObject target_object;
    public GameObject player;
    Vector3 target_transform;

    public List<Transform> limits;
            
    // Start is called before the first frame update
    void Start()
    {
        target_object = player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!= null)
        {
            if(player.transform.position.x < limits[0].position.x || player.transform.position.x > limits[1].position.x)
            {
                target_transform = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            }
            else
            {
                target_transform = new Vector3(target_object.transform.position.x, gameObject.transform.position.y, -10);
            }

            transform.position = Vector3.Lerp(this.transform.position, new Vector3(target_transform.x, target_transform.y, -10), 5f * Time.deltaTime);
        }
    }
}
