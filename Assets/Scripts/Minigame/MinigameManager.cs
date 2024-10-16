using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MinigameManager : MonoBehaviour
{
    private Player playerControl;
    private GameObject transition;

    private SpriteRenderer transitionSpriteRenderer;
    private GameObject[] lives;

    private int livesLost = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindFirstObjectByType<Player>();
        playerControl.StartRunning();
        GameObject[] transitions = GameObject.FindGameObjectsWithTag("Transition");
        transition = transitions[0];
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        lives = GameObject.FindGameObjectsWithTag("Soul");
        lives = lives.OrderByDescending(obj => obj.transform.position.x).ToArray();

        StartCoroutine(FadeAnimator.FadeIn(transitionSpriteRenderer, 1, 0, 3));

    }

    // Update is called once per frame
    void Update()
    {
        // end scene trigger
        if (playerControl.transform.position.x > transform.position.x)
        {
            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "EndingScene"));
        }

        if ((livesLost > 0) && (livesLost <= lives.Length))
        {
            StartCoroutine(UseUpLifeAnimation(lives[livesLost - 1]));
        }

        if (livesLost > lives.Length)
        {
            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "Main"));
        }

    }

    public void LoseLife()
    {
        livesLost++;
    }

    private IEnumerator UseUpLifeAnimation(GameObject miracle)
    {
        Vector3 start = miracle.transform.position;
        Vector3 target = playerControl.transform.position;
        SpriteRenderer lifeSpriteRenderer = miracle.GetComponent<SpriteRenderer>();

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;
            if (t > 1) t = 1;
            miracle.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        StartCoroutine(FadeAnimator.FadeIn(lifeSpriteRenderer, 1.0f, 0f, 1.0f));
        lifeSpriteRenderer.enabled = false;
    }

}
