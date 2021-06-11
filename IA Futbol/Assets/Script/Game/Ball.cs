using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    Vector3 initialPosition;
    [SerializeField] LayerMask campoLayer;
    const float maxBallOnAirTime = 1;
    float timeOnAir = 0;
    bool ballOnAir = true;
    public void setBallOnAir(bool ballMoving)
    {
        ballOnAir = ballMoving;
    }

    public bool getBallOnAir()
    {
        return ballOnAir;
    }
    private void Update()
    {
        //Ponemos un tope al tiempo que puede estar el balón en el aire para que no sea una "bala perdida" hasta que colisione
        if (ballOnAir)
        {
            timeOnAir += Time.deltaTime;
            if (timeOnAir >= maxBallOnAirTime)
            {
                timeOnAir = 0;
                ballOnAir = false;
            }
        }
    }
    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    public void reset()
    {
        transform.position = initialPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) != campoLayer)
        {

            ballOnAir = false;
            FootBallPlayer player = collision.gameObject.GetComponent<FootBallPlayer>();
            if (player && !player.isStunned())
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                FootBallPlayer owner = GameManager.getInstance().getBallOwner();
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
