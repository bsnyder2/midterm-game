using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Animation
    public Dictionary<string, Sprite[]> animations;
    SpriteRenderer _playerSprite;
    public Sprite[] walk;
    public Sprite[] idle;
    public Sprite[] hit;
    public Sprite[] attack;
    public Sprite[] die;
    public float frameRate = 0.1f;
    private bool isMoving = false;
    private bool isIdle = true;
    private Sprite[] currentAnimation;

    // Player Movement
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        animations = new Dictionary<string, Sprite[]>();
        animations.Add("Walk", walk);
        animations.Add("Idle", idle);
        animations.Add("Hit", hit);
        animations.Add("Attack", attack);
        animations.Add("Die", die);

        //MoveAndAnimate(transform.position, transform.position + (2 * Vector3.right), "Walk");
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            MoveAndAnimate(transform.position, transform.position, "Idle");
        }
    }

    public void MoveAndAnimate(Vector3 startPosition, Vector3 endPosition, string animationName)
    {
        if (isMoving)
        {
            isIdle = false;
            return;
        }

        StartCoroutine(MoveAndPlayAnimation(startPosition, endPosition, animationName));
    }

    private IEnumerator MoveAndPlayAnimation(Vector3 startPosition, Vector3 endPosition, string animationName)
    {
        isMoving = true;

        if (!animations.ContainsKey(animationName))
        {
            Debug.Log("Animation not found");
            yield break;
        }

        Sprite[] currentAnimation = animations[animationName];
        float distance = Vector3.Distance(startPosition, endPosition);
        float progress = 0f;
        int currentFrame = 0;

        transform.position = startPosition;

        while (progress < 1f)
        {
            progress += (speed * Time.deltaTime) / distance;
            Debug.Log("Progress = " + progress);
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            Debug.Log("Position = " + transform.position);

            if (frameRate > 0)
            {
                yield return new WaitForSeconds(frameRate);
                currentFrame++;

                if (currentFrame >= currentAnimation.Length)
                {
                    currentFrame = 0;
                }

                _playerSprite.sprite = currentAnimation[currentFrame];
            }

            yield return null;
        }

        transform.position = endPosition;
        isMoving = false;
        isIdle = true;
    }

}
