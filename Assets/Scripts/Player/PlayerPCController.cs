using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//управление игрой для pc
public class PlayerPCController : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    void Update()
    {
        playerManager.move = Input.GetAxis("Horizontal");
        playerManager.jump = Input.GetButtonDown("Jump");
        playerManager.dash = Input.GetButtonDown("Dash");
        playerManager.hit = Input.GetButtonDown("Fire1");
        playerManager.fire = Input.GetButtonDown("Fire2");
    }

}
