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

    public void Die()
    {
        enemyAnimator.Die();
    }
}
