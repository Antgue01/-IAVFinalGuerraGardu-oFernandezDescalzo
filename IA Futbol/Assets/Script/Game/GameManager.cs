using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team : uint { TeamA = 0, TeamB = 1, Nobody = 2 };
public enum Zone : uint { Goal, Attack, Center, Defense };
public enum Rol : uint { Delantero = 0, Centro = 1, Defensa = 2 };
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            initTeams();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void initTeams()
    {
        foreach (Transform player in teamA)
        {
            FootBallPlayer football = player.gameObject.GetComponent<FootBallPlayer>();
            if (football)
                TeamAPlayers.Add(football);
            else
            {
                GoalKeeper goalKeeper = player.gameObject.GetComponent<GoalKeeper>();
                if (goalKeeper)
                    goalKeeperTeamA = goalKeeper;
            }
        }
        foreach (Transform player in teamB)
        {
            FootBallPlayer football = player.gameObject.GetComponent<FootBallPlayer>();
            if (football)
                TeamBPlayers.Add(football);
            else
            {
                GoalKeeper goalKeeper = player.gameObject.GetComponent<GoalKeeper>();
                if (goalKeeper)
                    goalKeeperTeamB = goalKeeper;
            }
        }
    }

    const int MaxGoals = 3;
    int[] goals = new int[2];
    [SerializeField] Text WinnerText;
    [SerializeField] Text[] teams = new Text[2];
    [SerializeField] string[] names = new string[2];
    [SerializeField] Transform teamA, teamB;
    [SerializeField] Transform zonaA, zonaB, zonaMedio, goalZoneA, goalZoneB, ATKTeamA, ATKTeamB;
    List<FootBallPlayer> TeamAPlayers = new List<FootBallPlayer>();
    List<FootBallPlayer> TeamBPlayers = new List<FootBallPlayer>();
    [SerializeField] Ball ball;
    [SerializeField] Collider campo;
    GoalKeeper goalKeeperTeamA;
    GoalKeeper goalKeeperTeamB;
    FootBallPlayer ballOwner;
    bool ballOnAir = true;

    public void notifyGoalKeeper(Team shooterTeam)
    {
        if (shooterTeam == Team.TeamA)
            goalKeeperTeamB.Catch();
        else goalKeeperTeamA.Catch();
    }
    public static GameManager getInstance()
    {
        if (_instance == null)
        {
            throw new System.Exception("Singleton not created");
        }
        return _instance;
    }

    public void Goal(Team team)
    {
        ballOnAir = false;

        uint teamN = (uint)team;

        goals[teamN]++;
        teams[teamN].text = goals[teamN].ToString();
        if (goals[teamN] >= MaxGoals)
            WinnerText.text = "Gana " + names[1 - teamN];
        reset();
    }

    public List<FootBallPlayer> getTeam(Team team)
    {
        if (team == Team.TeamA)
            return TeamAPlayers;
        else return TeamBPlayers;
    }

    public float getDefenseZone(Team team, Rol role)
    {
        if (role == Rol.Centro)
            return zonaMedio.position.x;
        else if (team == Team.TeamA)
        {
            if (role == Rol.Defensa) return zonaA.position.x;
            else return zonaB.position.x;
        }
        else
        {
            if (role == Rol.Defensa) return zonaB.position.x;
            else return zonaA.position.x;
        }
    }
    public float getCenterZone()
    {
        return zonaMedio.position.x;
    }
    public float getAttackZone(Team team, Rol role)
    {
        if (team == Team.TeamA)
        {
            if (role == Rol.Centro)
                return zonaB.position.x;
            else if (role == Rol.Defensa) return zonaA.position.x;
            else return ATKTeamA.position.x;
        }
        else
        {
            if (role == Rol.Centro)
                return zonaA.position.x;
            else if (role == Rol.Defensa) return zonaB.position.x;
            else return ATKTeamB.position.x;
        }

    }
    void reset()
    {
        foreach (FootBallPlayer player in TeamAPlayers)
        {
            player.reset();
        }
        foreach (FootBallPlayer player in TeamBPlayers)
        {
            player.reset();
        }
        goalKeeperTeamA.reset();
        goalKeeperTeamB.reset();
        ball.reset();
    }
    public Collider getCampo() { return campo; }

    public Transform getGoalZone(Team team)
    {
        if (team == Team.TeamA)
        {
            return goalZoneA;
        }
        else
        {
            return goalZoneB;
        }
    }

    public void setBallOnAir(bool ballMoving)
    {
        ballOnAir = ballMoving;
    }

    public bool getBallOnAir()
    {
        return ballOnAir;
    }

    public Vector3 getBallPosition() { return ball.transform.position; }

    public void setBallOwner(FootBallPlayer player) { ballOwner = player; }
    public FootBallPlayer getBallOwner() { return ballOwner; }
}
