using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class FadeAnimator
{
    //public float fadeTime = 12;
    //private SpriteRenderer spriteRenderer;

    //Start is called before the first frame update
    //void Start()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    StartCoroutine(FadeIn());
    //}

    public static IEnumerator FadeIn(SpriteRenderer spriteRenderer, float alphaStart, float alphaEnd, float fadeTime)
    {
        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(alphaStart, alphaEnd, timer / fadeTime);
            Color color = spriteRenderer.material.color;
            spriteRenderer.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }

    public static IEnumerator FadeIntoTransition(SpriteRenderer spriteRenderer, float alphaStart, float alphaEnd, float fadeTime, string nextScene)
    {
        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(alphaStart, alphaEnd, timer / fadeTime);
            Color color = spriteRenderer.material.color;
            spriteRenderer.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }
}
