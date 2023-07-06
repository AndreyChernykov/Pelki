using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject fireball;
    [SerializeField] float shotFrequency;
    [SerializeField] Transform shootPoint;

    IEnumerator fire;

    void Start()
    {
        fire = Fire();
        StartCoroutine(fire);
    }

    IEnumerator Fire()
    {
        while (true)
        {    
            if (enemy.isAttack) 
            {
                Instantiate(fireball, shootPoint.position, shootPoint.rotation);
                yield return new WaitForSeconds(shotFrequency);
            }
            yield return new WaitForFixedUpdate();

        }
        
    }


}
