using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public bool isTarget;
    public Sprite[] standardFrames;
    public Sprite[] targetFrames;
    public float animationFrameTime;
    private Sprite[] frames;

    private SpriteRenderer spriteRenderer;
    private Queue<IEnumerator> animationQueue;

    // *Awake is called when script instance is loaded
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animation queue is always active
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());

    }

    //public void BadFunction(bool isTarget)
    //{
    //    frames = isTarget ? targetFrames : standardFrames;
    //}

    private void Update()
    {
        // on switch to other set of frames/if isTarget changes
        //Sprite[] newFrames = isTarget ? targetFrames : standardFrames;
        //if ((frames != null) && (newFrames != frames))
        //{
        //    //spriteRenderer.sprite = newFrames[0];
        //    Close();
        //}
        //frames = newFrames;
        //Debug.Log(isTarget);
    }

    public void Open()
    {
        animationQueue.Enqueue(OpenRoutine());
    }

    public void Close()
    {
        animationQueue.Enqueue(CloseRoutine());
    }

    // inspired by https://discussions.unity.com/t/how-to-stack-coroutines-and-call-each-one-till-all-are-executed/219063/5
    IEnumerator AnimationCoordinator()
    {
        // constantly check...
        while (true)
        {
            // while animations left in the queue
            while (animationQueue.Count > 0)
            {
                // run next animation
                yield return StartCoroutine(animationQueue.Dequeue());
            }
            yield return null;
        }
    }

    IEnumerator OpenRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = 0;
        while (animationFrameIndex < standardFrames.Length)
        {
            spriteRenderer.sprite = standardFrames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

    IEnumerator CloseRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = standardFrames.Length - 1;
        while (animationFrameIndex >= 0)
        {
            spriteRenderer.sprite = standardFrames[animationFrameIndex];
            animationFrameIndex--;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

}
