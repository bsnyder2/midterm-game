using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //// singleton (read only)
    //public static CameraManager Instance { get; private set; }
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
        transform.position = new Vector3(playerControl.transform.position.x, transform.position.y, transform.position.z);
        //= playerControl.transform.position.x;
    }
}
