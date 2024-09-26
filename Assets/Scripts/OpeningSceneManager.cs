using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    // Start is called before the first frame update
    void Start()
    {
        //move character off screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // if character hits a certain position off screen
        {
            StartCoroutine(Activate());
        }

        //if character hits a certain position off screen, load next scene which is the gameplay 
    }

    IEnumerator Activate()
    {
        foreach (GameObject go in objectsToActivate)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
