using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    //private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        //rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(MoveRight());
        }
    }

    public void Attack()
    {
        // playerAnimator.PlayAnimation("attack");
        playerAnimator.Attack();
    }

    private IEnumerator MoveRight()
    {
        //float lerpDuration = 1f;
        //float timeElapsed = 0;
        //float interp = 0;
        //while (timeElapsed < lerpDuration)
        //{
        //    interp = Mathf.Lerp(transform.position.x, transform.position.x + 1, timeElapsed / lerpDuration);
        //    timeElapsed += Time.deltaTime;
        //    transform.position.x = interp;
        //    yield return null;
        //}
        //interp = transform.position.x + 1;

        float t = 0f;

        float x = 0.5f;

        Vector3 start = transform.position;
        Vector3 target = new Vector3(start.x + 1, start.y, start.z);

        while (t < 1)
        {
            t += Time.deltaTime / x;
            if (t > 1) t = 1;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }


        // Distance moved equals elapsed time times speed..
        //float distCovered = (Time.time - startTime) * speed;

        //// Fraction of journey completed equals current distance divided by total distance.
        //float fractionOfJourney = distCovered / journeyLength;

        //// Set our position as a fraction of the distance between the markers.
        //transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

        //float interp = 0;
        //float speed = 1f;
        //float distance = 1f;
        //while (interp <= 1)
        //{
        //    interp += (speed * Time.deltaTime) / distance;
        //    transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right, interp);
        //    Debug.Log(interp);
        //    interp += Time.deltaTime;
        //    yield return null;
        //}

    }
}
