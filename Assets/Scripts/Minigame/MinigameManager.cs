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
        Debug.Log("lives lost are " + livesLost);
        if (playerControl.transform.position.x > transform.position.x)
        {
            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "EndingScene"));
        }

        //LoseLife();
        //Debug.Log("Help " + playerController.isMoving);

        //for testing comment out later: 
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log("Escape Key Pressed");
        //    LoseLife();
        //}

        if ((livesLost > 0) && (livesLost <= lives.Length))
        {
            //lives[livesLost - 1].Activate(false);
            StartCoroutine(UseUpLifeAnimation(lives[livesLost - 1]));
        }

        if (livesLost > lives.Length)
        {
            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "Opening1"));
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

        lifeSpriteRenderer.enabled = false;
    }

}
