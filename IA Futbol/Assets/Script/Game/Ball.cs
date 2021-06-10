using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    FootBallPlayer owner;
    Vector3 initialPosition;
    [SerializeField] LayerMask campoLayer;
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
        if(LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) != campoLayer)
        {
            GameManager.getInstance().setBallOnAir(false);
            FootBallPlayer player = collision.gameObject.GetComponent<FootBallPlayer>();
            if (player && !player.isStunned())
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                if (owner != null)
                {
                    owner.setHasBall(null);
                    owner.stun();

                }

                player.setHasBall(this);
                if (player.getWaitingForPass())
                    player.setWaitingForPass(false);
            }
        }
    }
}
