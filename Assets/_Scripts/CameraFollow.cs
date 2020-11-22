using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, transform.position.y,transform.position.z);
        if(transform.position != new Vector3(player.transform.position.x, transform.position.y, transform.position.z))
        {
            //transform.Translate(new Vector3(((player.transform.position.x-transform.position.x)/.5f)*Time.deltaTime,0,0));
            //Vector3.SmoothDamp
            transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, -10), 
                new Vector3(player.transform.position.x, player.transform.position.y+2.25f, -10),ref velocity, 0.4f);
        }
    }
}
