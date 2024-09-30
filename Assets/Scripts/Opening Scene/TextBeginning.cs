using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBeginning : MonoBehaviour
{
    public GameObject[] frames;
    private List<GameObject> group = new List<GameObject>();
    public GameObject blinkTransition;
    public float fadeDuration = 1.0f;      // Time it takes to fade in/out
    public float stayDuration = 2.0f;      // Time the sprite stays fully visible

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartStory());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartStory()
    {

        int animationFrameIndex = 0;
        while (animationFrameIndex < frames.Length)
        {
            if (animationFrameIndex == 5)
            {
                group.Add(frames[animationFrameIndex - 1]);
                group.Add(frames[animationFrameIndex - 2]);
                yield return StartCoroutine(FadeSprite(group, 1f, 0f));
            }

            else if (animationFrameIndex != 0 && animationFrameIndex != 4)
            {
                group.Add(frames[animationFrameIndex - 1]);
                yield return StartCoroutine(FadeSprite(group, 1f, 0f));

            }

            if (group != null)
            {
                group.Clear();
            }

            group.Add(frames[animationFrameIndex]);
            yield return StartCoroutine(FadeSprite(group, 0f, 1f));
            animationFrameIndex++;

            if (animationFrameIndex == frames.Length)
            {
                yield return StartCoroutine(FadeSprite(group, 1f, 0f));
                yield return new WaitForSeconds(1);
                blinkTransition.SetActive(true);
                gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(stayDuration);
        }
    }

    private IEnumerator FadeSprite(List<GameObject> frame, float startAlpha, float endAlpha)
    {
        SpriteRenderer[] spriteRenderer = new SpriteRenderer[frame.Count];
        for (int i = 0; i < frame.Count; i++)
        {
            spriteRenderer[i] = frame[i].GetComponent<SpriteRenderer>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            foreach (SpriteRenderer s in spriteRenderer)
            {
                Color color = s.color;
                color.a = alpha;
                s.color = color;
            }

            yield return null;
        }

        foreach (SpriteRenderer s in spriteRenderer)
        {
            Color color = s.color;
            color.a = endAlpha;
            s.color = color;
        }
    }
}
