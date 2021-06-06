using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    FootBallPlayer owner;
    Vector3 initialPosition;
    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    public void reset()
    {
        transform.position = initialPosition;
        owner = null;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
