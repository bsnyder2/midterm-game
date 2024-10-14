using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenManager : MonoBehaviour
{
    public GameObject text;
    private SpriteRenderer textSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        textSpriteRenderer = text.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAnimator.FadeIn(textSpriteRenderer, 0, 1, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
