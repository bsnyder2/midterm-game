using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public Sprite[] frames;
    public float animationFrameTime;
    //private bool moving;
    //public bool open;

    private SpriteRenderer spriteRenderer;

    private Queue<IEnumerator> animationQueue;

    //private float animationFrameTimer;
    //private int animationFrameIndex;

    // *Awake is called when script instance is loaded
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open()
    {
        //if (open) return;
        //open = true;
        //StopCoroutine(CloseRoutine());
        //if (moving) return;
        animationQueue.Enqueue(OpenRoutine());

        // messes up when second one hasn't finished its animation
    }

    public void Close()
    {
        //if (!open) return;
        //open = false;
        //StopCoroutine(OpenRout, ine()); ;
        //if (moving) return;
        animationQueue.Enqueue(CloseRoutine());
        //StartCoroutine(CloseRoutine());
    }

    // https://discussions.unity.com/t/how-to-stack-coroutines-and-call-each-one-till-all-are-executed/219063/5
    IEnumerator AnimationCoordinator()
    {
        // constantly check...
        while (true)
        {
            // while coroutines left in the queue
            while (animationQueue.Count > 0)
            {
                yield return StartCoroutine(animationQueue.Dequeue());
            }
            yield return null;
        }
    }

    IEnumerator OpenRoutine()
    {
        //moving = true;
        // play all frames of animation
        int animationFrameIndex = frames.Length - 1;
        while (animationFrameIndex >= 0)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex--;
            //yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(Time.deltaTime * 10);
        }
        //moving = false;
    }

    IEnumerator CloseRoutine()
    {
        //moving = true;
        // play all frames of animation
        int animationFrameIndex = 0;
        while (animationFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * 5);
            //yield return new WaitForSeconds(animationFrameTime);
        }
        //moving = false;
    }

}