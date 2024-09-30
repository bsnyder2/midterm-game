using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);

        enemyAnimator = GetComponent<EnemyAnimator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Debug.Log("enemy died");
        enemyAnimator.Die();
        //Destroy(gameObject);
    }
}
