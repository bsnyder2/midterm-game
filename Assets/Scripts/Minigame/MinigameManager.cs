using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject character;
    public GameObject transition;
    private SpriteRenderer transitionSpriteRenderer;

    private Player playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = character.GetComponent<Player>();
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAnimator.FadeIn(transitionSpriteRenderer, 1, 0, 3));
    }

    // Update is called once per frame
    void Update()
    {


    }


}
