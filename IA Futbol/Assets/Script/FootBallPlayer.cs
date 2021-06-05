﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBallPlayer : MonoBehaviour
{
    bool hasBall = false;
    Ball myBall;
    bool dangered = false;
    List<FootBallPlayer> team;
    float limitAttackX, limitDefenseX;

    [SerializeField] Team myTeam;
    [SerializeField] Rol myRol;
    [SerializeField] BoxCollider goalZone;
    [SerializeField] float ShootPower = 2;
    [SerializeField] float PassPower = .2f;
    Vector3 shootDirection;


    private void Start()
    {
        team = GameManager.getInstance().getTeam(myTeam);
        limitAttackX = GameManager.getInstance().getAttackZone(myTeam);
        limitDefenseX = GameManager.getInstance().getDefenseZone(myTeam);
    }

    private void Update()
    {
        if (hasBall)
        {
            myBall.transform.position = transform.position + transform.forward;
        }
    }
    public void Shoot()
    {
        if (hasBall && shootDirection != Vector3.zero)
        {
            Rigidbody ballrb = myBall.GetComponent<Rigidbody>();
            ballrb.AddForce(shootDirection * ShootPower, ForceMode.Impulse);
        }
    }
    public void Pass(FootBallPlayer mate)
    {
        if (mate != this && myTeam == mate.myTeam)
        {
            Vector2 dir = mate.transform.position - transform.position;
            if (hasBall)
            {
                Rigidbody ballrb = myBall.GetComponent<Rigidbody>();
                ballrb.AddForce(dir.normalized * dir.magnitude * PassPower, ForceMode.Impulse);
                hasBall = false;
            }
        }
    }
    public Team getMyTeam() { return myTeam; }
    public bool isInDanger() { return dangered; }
    public void setDangered(bool v) { dangered = v; }
    public BoxCollider getGoalZone() { return goalZone; }
    public void setShootDirection(Vector3 dir) { shootDirection = dir; }
    public Vector3 getShootDirection() { return shootDirection; }
    public List<FootBallPlayer> getTeam() { return team; }
    public Vector3 getLimitAttack() { return new Vector3(limitAttackX, transform.position.x, transform.position.z); }
    public Vector3 getLimitDefense() { return new Vector3(limitDefenseX, transform.position.x, transform.position.z); }
    public void setHasBall(bool b, Ball ball) { hasBall = b; if (b) myBall = ball; else myBall = null; }
    public bool getHasBall() { return hasBall; }
}
