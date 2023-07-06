using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]Enemy enemy;

    int damage;

    void Start()
    {
        damage = enemy.damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().Damage(0, damage);
        }
    }

}
