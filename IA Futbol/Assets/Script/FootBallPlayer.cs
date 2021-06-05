using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FootBallPlayer : MonoBehaviour
{
    bool hasBall = false;
    bool dangered = false;
    List<FootBallPlayer> team;
    float limitAttackX, limitDefenseX;

    [SerializeField] Team myTeam;
    [SerializeField] Rol myRol;
    [SerializeField] BoxCollider goalZone;
    public void setHasBall(bool b) { hasBall = b; }
    public bool getHasBall() { return hasBall; }
    [SerializeField] float ShootPower = 2;
    [SerializeField] float PassPower = .2f;
    Vector3 shootDirection;


    public void Start()
    {
        team = GameManager.getInstance().getTeam(myTeam);
        limitAttackX = GameManager.getInstance().getAttackZone(myTeam);
        limitDefenseX = GameManager.getInstance().getDefenseZone(myTeam);
    }
    public Team getMyTeam()
    {
        return myTeam;
    }
    public bool isInDanger()
    {
        return dangered;
    }
    public void setDangered(bool v)
    {
        dangered = v;
    }
    public void Shoot()
    {
        if (hasBall && shootDirection != Vector3.zero)
        {
            Rigidbody2D ball = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            if (ball)
                ball.AddForce(shootDirection * ShootPower, ForceMode2D.Impulse);
        }
    }
    public void Pass(FootBallPlayer mate)
    {
        if (mate != this && myTeam == mate.myTeam)
        {
            Rigidbody2D ball = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            Vector2 dir = mate.transform.position - transform.position;
            if (ball)
                ball.AddForce(dir.normalized * dir.magnitude * PassPower, ForceMode2D.Impulse);

        }
    }
    public BoxCollider getGoalZone() { return goalZone; }
    public void setShootDirection(Vector3 dir) { shootDirection = dir; }
    public Vector3 getShootDirection() { return shootDirection; }
    public List<FootBallPlayer> getTeam() { return team; }
    public Vector3 getLimitAttack() { return new Vector3(limitAttackX, transform.position.x, transform.position.z); }
    public Vector3 getLimitDefense() { return new Vector3(limitDefenseX, transform.position.x, transform.position.z); }
}
