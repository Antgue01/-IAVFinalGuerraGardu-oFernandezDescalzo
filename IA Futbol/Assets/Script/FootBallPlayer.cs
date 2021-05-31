using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FootBallPlayer : MonoBehaviour
{
    bool hasBall = false;
    enum Team { TeamA, TeamB };
    [SerializeField] Team myTeam;

    public void setHasBall(bool b) { hasBall = b; }
    public bool getHasBall() { return hasBall; }
    [SerializeField] float ShootPower = 2;
    [SerializeField] float PassPower = .2f;
    public void Shoot(Vector2 dir)
    {
        if (hasBall)
        {
            Rigidbody2D ball = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            if (ball)
                ball.AddForce(dir * ShootPower, ForceMode2D.Impulse);
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
}
