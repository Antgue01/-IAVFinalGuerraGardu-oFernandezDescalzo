using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootBallPlayer : MonoBehaviour
{
    bool hasBall = false;
    Ball myBall;
    bool dangered = false;
    bool stunned = false;
    bool waitingForPass = false;
    List<FootBallPlayer> team;
    float limitAttackX, limitDefenseX;
    NavMeshAgent navmesh;

    [SerializeField] Team myTeam;
    [SerializeField] Rol myRol;
    [SerializeField] BoxCollider goalZone;
    [SerializeField] float ShootPower = 2;
    [SerializeField] float PassPower = .2f;
    [SerializeField] float spreadDistance = 1.2f;
    [SerializeField] float stunnedTime = 1;
    [SerializeField] float MaxWaitingTime = 2.25f;
    float timeWaiting = 0;
    float timeSinceLast = 0;
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
        stunned = false;

    }
    public void goTo(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 0.05 && !waitingForPass)
        {
            navmesh.isStopped = false;
            navmesh.SetDestination(target.position);
        }
    }
    public void goTo(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > 0.05 && !waitingForPass)
        {
            navmesh.isStopped = false;
            navmesh.SetDestination(target);
        }
    }
    private void Update()
    {
        if (hasBall)
        {
            myBall.transform.position = new Vector3(transform.position.x + transform.forward.x, myBall.transform.position.y, 
                transform.position.z + transform.forward.z);

        }
        if (stunned)
        {
            timeSinceLast += Time.deltaTime;
            if (timeSinceLast >= stunnedTime)
            {
                stunned = false;
                timeSinceLast = 0;
                Debug.Log("Patata");
            }
        }
        if (waitingForPass)
        {
            timeWaiting += Time.deltaTime;
            if (timeWaiting >= MaxWaitingTime)
            {
                timeWaiting = 0;
                waitingForPass = false;
            }
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
            setHasBall(null);
            GameManager.getInstance().notifyGoalKeeper(myTeam);
            GameManager.getInstance().setBallOnAir(true);
        }
    }
    public void Pass(FootBallPlayer mate)
    {
        if (mate != this && myTeam == mate.myTeam)
        {
            mate.setWaitingForPass(true);
            Vector3 dir = mate.transform.position - transform.position;
            mate.navmesh.isStopped = true;
            if (hasBall)
            {
                Rigidbody ballrb = myBall.GetComponent<Rigidbody>();
                transform.LookAt(mate.transform.position);
                myBall.transform.position = transform.position + transform.forward;
                ballrb.AddForce(dir.normalized * dir.magnitude * PassPower, ForceMode.Impulse);
                setHasBall(null);
                GameManager.getInstance().setBallOnAir(true);
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
    public bool isStunned() { return stunned; }
    public void stun() { stunned = true; Debug.Log("stunned"); }
    public bool getWaitingForPass() { return waitingForPass; }
    public void setWaitingForPass(bool b) {
        if (b) GetComponent<Rigidbody>().velocity = Vector3.zero;
        waitingForPass = b; 
    }
    public void setHasBall(Ball ball)
    {
        hasBall = ball != null;
        if (hasBall)
        {
            GameManager.getInstance().setBallOwner(this);
            timeWaiting = 0;
            waitingForPass = false;
        }
        else
        {
            GameManager.getInstance().setBallOwner(null);
        }
        myBall = ball;
    }
    public bool getHasBall() { return hasBall; }
    public void spread()
    {
        Vector3 defaultPos = new Vector3(limitAttackX, transform.position.y, transform.position.z);
        FootBallPlayer owner = GameManager.getInstance().getBallOwner();
        if (owner != null && owner.getMyTeam() == myTeam)
        {

            RaycastHit info;
            Vector3 dir = owner.transform.position - transform.position;
            Debug.DrawRay(transform.position, dir, Color.magenta);

            if (Physics.Raycast(transform.position, dir.normalized, out info, dir.magnitude, LayerMask.GetMask(LayerMask.LayerToName(this.gameObject.layer))) &&
                info.collider.gameObject.GetComponent<FootBallPlayer>().myTeam != myTeam)
            {
                Vector3 orthogonal1 = transform.position + new Vector3(-dir.z, dir.y, dir.x).normalized * spreadDistance;
                Vector3 orthogonal2 = transform.position + new Vector3(dir.z, dir.y, -dir.x).normalized * spreadDistance;
                goTo(Vector3.Distance(orthogonal1, goalZone.transform.position) < Vector3.Distance(orthogonal2, goalZone.transform.position) ? orthogonal1 : orthogonal2);
            }
            else goTo(defaultPos);

        }
        else goTo(defaultPos);
    }

    public void coverPlayer(Vector3 enemyPos)
    {
        Vector3 dir = (GameManager.getInstance().getBallPosition() - enemyPos).normalized;
        goTo(enemyPos + dir * (GetComponent<Collider>().bounds.extents.x * 2));
    }
}
