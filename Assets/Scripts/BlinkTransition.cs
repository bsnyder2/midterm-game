using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTransition : MonoBehaviour
{
    public Sprite[] frames;
    public float animationFrameTime;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkOpen());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator BlinkOpen()
    {
        int animationFrameIndex = 0;
        while (animationFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(animationFrameTime);
        }
        gameObject.transform.localScale += new Vector3(5, 7, 0); ;
        yield return new WaitForSeconds(animationFrameTime);
        gameObject.SetActive(false);
    }

}