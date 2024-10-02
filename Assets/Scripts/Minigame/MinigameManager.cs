using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public GameObject character;
    public GameObject transition;

    //public GameObject lastLife
    private SpriteRenderer transitionSpriteRenderer;
    public GameObject[] lives;
    //private SpriteRenderer[] lifeSpriteRenderer;

    private Player playerController;

    private int livesLost = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = character.GetComponent<Player>();
        //lifeSpriteRenderer = lastLife.GetComponent<SpriteRenderer>();
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        Debug.Log("Help " + playerController.isMoving);
        StartCoroutine(FadeAnimator.FadeIn(transitionSpriteRenderer, 1, 0, 3));
        playerController.isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Help " + playerController.isMoving);

        //for testing comment out later: 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoseLife();
        }

        if (livesLost > 0 && livesLost <= lives.Length)
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
        Vector3 target = character.transform.position;
        SpriteRenderer lifeSpriteRenderer = miracle.GetComponent<SpriteRenderer>();

        Debug.Log("Should be moving");

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
