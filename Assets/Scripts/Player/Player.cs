using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        //Awake();
        // should be more like
        // playerAnimator.PlayAnimation("attack");
        //Debug.Log(playerAnimator);
        playerAnimator.Attack();
    }
}
