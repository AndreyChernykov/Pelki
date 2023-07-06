using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionLight : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D boxCollider;

    Transform target;
    PlayerManager playerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (target == null)target = playerManager.GetPointCompanion();
            playerManager.TakeACompanion();
            boxCollider.enabled = false;
        }
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (target != null)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
