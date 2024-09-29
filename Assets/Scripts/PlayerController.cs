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
    private bool animating = false;
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
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MoveAndAnimate(Vector3 startPosition, Vector3 endPosition, string animationName)
    {
        if (animating)
       {
            return;
       }

       StartCoroutine(MoveAndPlayAnimation(startPosition, endPosition, animationName));
    }

    private IEnumerator MoveAndPlayAnimation(Vector3 startPosition, Vector3 endPosition, string animationName)
    {
        animating = true;

        if(!animations.ContainsKey(animationName))
        {
            Debug.Log("Animation not found");
            yield break;
        }

        Sprite[] currentAnimation = animations[animationName];
        float distance = Vector3.Distance(startPosition, endPosition);
        float progress = 0f;
        int currentFrame = 0;

        transform.position = startPosition;

        while(progress < 1f)
        {
            progress += (speed * Time.deltaTime) / distance;
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);

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
        animating = false;
    }

}
