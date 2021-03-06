using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 5;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        Die();
    }

    private void Die()
    {
        if(currentHP <= 0)
        {
            gameObject.GetComponent<EnemyAI>().Die();
        }
    }

    public void HurtEnemy(float damage)
    {
        currentHP -= damage;
        gameObject.GetComponent<EnemyAI>().Slash();
        gameObject.GetComponent<EnemyAI>().activated = true;
        gameObject.GetComponent<EnemyAI>().activateDistance = 23;
        Debug.Log(currentHP);
    }

}
