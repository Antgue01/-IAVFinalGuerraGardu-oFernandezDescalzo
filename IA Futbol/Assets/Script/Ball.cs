using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    FootBallPlayer owner;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FootBallPlayer player = collision.gameObject.GetComponent<FootBallPlayer>();
        if (player)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (owner != null)
            {
                owner.setHasBall(false, this);
            }
            player.setHasBall(true, this);
        }
    }
}
