//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OpeningSceneManager : MonoBehaviour
//{
//    public GameObject[] objectsToActivate;
//    public GameObject character;
//    public Transform endPosition;

//    private Vector3 doneTrigger;
//    private bool soulsActivated = false;
//    public GameObject transition;
//    private SpriteRenderer transitionSpriteRenderer;

//    private Player playerController;
//    private bool isDone = false;

//    private string nextScene = "Main";
//    // Start is called before the first frame update
//    void Start()
//    {
//        //move character off screen
//        playerController = character.GetComponent<Player>();
//        doneTrigger = objectsToActivate[2].transform.position;
//        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
//        playerController.isMoving = true;
//        //playerController.enabled = true;
//        //playerController.Run();
//        //playerController.MoveAndAnimate(character.transform.position, endPosition.position, "Walk");
//        //Debug.Log("Started");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        /*if (Input.GetKeyDown(KeyCode.R)) // if character hits a certain position off screen
//        {
//            StartCoroutine(Activate());
//        }*/

//        Debug.Log(character.transform.position.x);

//        if (character.transform.position.x >= endPosition.position.x && soulsActivated == false)
//        {
//            StartCoroutine(Activate());

//            //Debug.Log("Done");
//        }

//        if (objectsToActivate[2].transform.position == doneTrigger && isDone == true)
//        {
//            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, nextScene));
//            isDone = false;
//        }

//        //if character hits a certain position off screen, load next scene which is the gameplay 
//    }

//    IEnumerator Activate()
//    {
//        foreach (GameObject go in objectsToActivate)
//        {
//            go.SetActive(true);
//            yield return new WaitForSeconds(0.5f);
//        }

//        soulsActivated = true;
//        isDone = true;
//    }

//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject character;
    public Transform endPosition;

    private Vector3 doneTrigger;
    private bool soulsActivated = false;
    public GameObject transition;
    private SpriteRenderer transitionSpriteRenderer;

    private Player playerController;
    private bool isDone = false;

    private string nextScene = "Main";
    // Start is called before the first frame update
    void Start()
    {
        //move character off screen
        playerController = character.GetComponent<Player>();
        doneTrigger = objectsToActivate[2].transform.position;
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        playerController.isMoving = true;
        //playerController.enabled = true;
        //playerController.Run();
        //playerController.MoveAndAnimate(character.transform.position, endPosition.position, "Walk");
        //Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R)) // if character hits a certain position off screen
        {
            StartCoroutine(Activate());
        }*/

        Debug.Log(character.transform.position.x);

        if (character.transform.position.x >= endPosition.position.x && soulsActivated == false)
        {
            StartCoroutine(Activate());

            //Debug.Log("Done");
        }

        if (objectsToActivate[2].transform.position == doneTrigger && isDone == true)
        {
            StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, nextScene));
            isDone = false;
        }

        //if character hits a certain position off screen, load next scene which is the gameplay 
    }

    IEnumerator Activate()
    {
        foreach (GameObject go in objectsToActivate)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        soulsActivated = true;
        isDone = true;
    }

}
