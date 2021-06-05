using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FootBallPlayer footBall = other.gameObject.GetComponent<FootBallPlayer>();
        FootBallPlayer playerFootball = GetComponentInParent<FootBallPlayer>();
        if (footBall && playerFootball && footBall.getMyTeam() != playerFootball.getMyTeam())
        {
            Debug.Log("Detectado");
            playerFootball.setDangered(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        FootBallPlayer footBall = other.gameObject.GetComponent<FootBallPlayer>();
        FootBallPlayer playerFootball = GetComponentInParent<FootBallPlayer>();
        if (footBall && playerFootball && footBall.getMyTeam() != playerFootball.getMyTeam())
        {
            playerFootball.setDangered(false);
        }
    }
}
