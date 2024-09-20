using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        eye = Instantiate(eye, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
