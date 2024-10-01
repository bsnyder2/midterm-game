using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1;
    public float distance = 1f;
    private PlayerAnimator playerAnimator;

    //private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveRight());
    }

    public void Attack()
    {
        // playerAnimator.PlayAnimation("attack");
        playerAnimator.Attack();
    }

    private IEnumerator MoveRight()
    {
        Vector3 start = transform.position;
        Vector3 target = new Vector3(start.x + distance, start.y, start.z);

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            if (t > 1) t = 1;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

    }
}
