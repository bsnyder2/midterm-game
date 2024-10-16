using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float rotationPerFrame = -2f;
    private Player playerControl;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        transform.position = new Vector3(playerControl.transform.position.x + 2, transform.position.y, transform.position.z);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationPerFrame * Time.deltaTime));
    }
}
