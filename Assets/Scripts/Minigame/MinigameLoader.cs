using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLoader : MonoBehaviour
{
    private MinigameRunner minigameRunnerControl;


    // Start is called before the first frame update
    void Start()
    {
        minigameRunnerControl = GetComponent<MinigameRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        // trigger on some condition here
        minigameRunnerControl.running = true;
    }
}
