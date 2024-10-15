using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //// singleton (read only)
    //public static CameraManager Instance { get; private set; }
    public float rotationPerFrame = -2f;
    //public float rightPerFrame = 2f;

    private Player playerControl;

    // Start is called before the first frame update
    void Start()
    {
        // TODO is this bad practice
        playerControl = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += rightPerFrame * Vector3.right * Time.deltaTime;
        Rotate();
        transform.position = new Vector3(playerControl.transform.position.x + 2, transform.position.y, transform.position.z);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationPerFrame * Time.deltaTime));
    }
}
