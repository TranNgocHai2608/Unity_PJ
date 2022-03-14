using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(-0.49f, 6.83f, -8f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //offset the camera behind the object by adding to the object's position
        transform.position = player.transform.position + offset;
    }
}
