using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team : uint { TeamA = 0, TeamB = 1 };
public enum Rol : uint { Delantero = 0, Centro = 1, Defensa = 2, Portero = 3 };
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameManager getInstance()
    {
        if (_instance == null)
        {
            throw new System.Exception("Singleton not created");
        }
        return _instance;
    }

    const int MaxGoals = 3;
    int[] goals = new int[2];
    [SerializeField] Text WinnerText;
    [SerializeField] Text[] teams = new Text[2];
    [SerializeField] string[] names = new string[2];
    [SerializeField] Transform teamA, teamB;
    [SerializeField] Transform zonaA, zonaB, zonaMedio;
    public void Goal(Team team)
    {
        uint teamN = (uint)team;

        goals[teamN]++;
        teams[teamN].text = goals[teamN].ToString();
        if (goals[teamN] >= MaxGoals)
            WinnerText.text = "Gana " + names[1 - teamN];
    }

    public List<FootBallPlayer> getTeam(Team team)
    {
        List<FootBallPlayer> aux = new List<FootBallPlayer>();
        if (team == 0)
        {
            foreach (Transform child in teamA)
            {
                aux.Add(child.gameObject.GetComponent<FootBallPlayer>());
            }
        }
        else
        {
            foreach (Transform child in teamB)
            {
                aux.Add(child.gameObject.GetComponent<FootBallPlayer>());
            }
        }
        return aux;
    }

    public float getLimit(int idZona)
    {
        if(idZona == 0) //ZonaA
        {
            return zonaA.position.x;
        }
        else if(idZona == 1) //ZonaB
        {
            return zonaB.position.x;
        }
        else //Mediocampo
        {
            return zonaMedio.position.x;
        }
    }
}
