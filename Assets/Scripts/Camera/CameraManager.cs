using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //// singleton (read only)
    //public static CameraManager Instance { get; private set; }
    private Player playerControl;
    private float rotationPerFrame = -0.0002f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO is this bad practice
        playerControl = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerControl.transform.position.x, transform.position.y, transform.position.z);
        Rotate();
        //= playerControl.transform.position.x;
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationPerFrame));
    }
}
