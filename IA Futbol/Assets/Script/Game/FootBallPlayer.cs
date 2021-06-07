using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootBallPlayer : MonoBehaviour
{
    bool hasBall = false;
    Ball myBall;
    bool dangered = false;
    List<FootBallPlayer> team;
    float limitAttackX, limitDefenseX;
    NavMeshAgent navmesh;

    [SerializeField] Team myTeam;
    [SerializeField] Rol myRol;
    [SerializeField] BoxCollider goalZone;
    [SerializeField] float ShootPower = 2;
    [SerializeField] float PassPower = .2f;
    Vector3 initialPos;
    Vector3 shootDirection;


    private void Start()
    {
        initialPos = transform.position;
        team = GameManager.getInstance().getTeam(myTeam);
        limitAttackX = GameManager.getInstance().getAttackZone(myTeam, myRol);
        limitDefenseX = GameManager.getInstance().getDefenseZone(myTeam, myRol);
        navmesh = GetComponent<NavMeshAgent>();
    }
    public void reset()
    {
        transform.position = initialPos;
        hasBall = false;
        shootDirection = Vector3.zero;
        dangered = false;
        myBall = null;

    }
    public void goTo(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 0.05)
        {
            navmesh.SetDestination(target.position);
        }
    }
    public void goTo(Vector3 target)
    {
        navmesh.SetDestination(target);
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
            transform.LookAt(shootDirection + transform.position);
            myBall.transform.position = transform.position + transform.forward;
            Rigidbody ballrb = myBall.GetComponent<Rigidbody>();
            ballrb.AddForce(shootDirection.normalized * ShootPower * shootDirection.magnitude, ForceMode.Impulse);
            hasBall = false;
            myBall = null;
            GameManager.getInstance().notifyGoalKeeper(myTeam);
        }
    }
    public void Pass(FootBallPlayer mate)
    {
        if (mate != this && myTeam == mate.myTeam)
        {
            Vector3 dir = mate.transform.position - transform.position;

            if (hasBall)
            {
                Rigidbody ballrb = myBall.GetComponent<Rigidbody>();
                transform.LookAt(mate.transform.position);
                myBall.transform.position = transform.position + transform.forward;
                ballrb.AddForce(dir.normalized * dir.magnitude * PassPower, ForceMode.Impulse);
                myBall = null;
                hasBall = false;
            }
        }
    }
    public Team getMyTeam() { return myTeam; }
    public Rol getMyRol() { return myRol; }
    public bool isInDanger() { return dangered; }
    public void setDangered(bool v) { dangered = v; }
    public BoxCollider getGoalZone() { return goalZone; }
    public void setShootDirection(Vector3 dir) { shootDirection = dir; }
    public Vector3 getShootDirection() { return shootDirection; }
    public List<FootBallPlayer> getTeam() { return team; }
    public float getLimitAttack() { return limitAttackX; }
    public float getLimitDefense() { return limitDefenseX; }
    public void setHasBall(bool b, Ball ball) { hasBall = b; if (b) myBall = ball; else myBall = null; }
    public bool getHasBall() { return hasBall; }
}
