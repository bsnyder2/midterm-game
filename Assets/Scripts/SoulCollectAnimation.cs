using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollectAnimation : MonoBehaviour
{
    public Transform startingPoint;
    private Vector3 endingPoint;
    public float speed = 1.0f;    // Speed of movement

    private float timeElapsed = 0f;

    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        endingPoint = gameObject.transform.position;
        transform.position = startingPoint.position;

        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            // Increase time
            timeElapsed += Time.deltaTime * speed;

            // Interpolate between the start and end points
            transform.position = Vector3.Lerp(startingPoint.position, endingPoint, timeElapsed);

            // Optionally, if you only want to move once, you can clamp the time to prevent overshooting
            if (timeElapsed >= 1.0f)
            {
                timeElapsed = 1.0f;
            }
        }

        if (transform.position == endingPoint)
        {
            start = false;
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        start = true;
    }
}
