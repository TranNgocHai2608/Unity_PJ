using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private float turnSpeed = 90.0f;
    private float horizontalInput;
    private float forwardInput;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    public string inputID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //setup controller
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        //Move the vehicle forward
        //transform.Translate(0, 0, 1 * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        //Turn the vehicle left or right
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        //switch camera
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    } 
}
