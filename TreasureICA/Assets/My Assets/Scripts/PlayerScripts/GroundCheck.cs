using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public PlayerController player;

    private void OnTriggerStay(Collider other)
    {
        player.isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player.isGrounded = false;
    }
}
