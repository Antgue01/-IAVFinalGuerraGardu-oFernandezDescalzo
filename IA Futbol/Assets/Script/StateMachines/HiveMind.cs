using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMind : MonoBehaviour
{
    public FootBallPlayer NearestFootBallPlayer(Transform target, List<FootBallPlayer> team)
    {
        float targetDistance = float.MaxValue;
        FootBallPlayer nearest = null;
        foreach (FootBallPlayer player in team)
        {
            float distance = (target.position - player.transform.position).magnitude;
            if (distance < targetDistance)
            {
                targetDistance = distance;
                nearest = player;
            }
        }
        return nearest;
    }

    public bool hasPossession(List<FootBallPlayer> team)
    {
        bool hasPossession = false;
        int i = 0;
        while (i < team.Count && !hasPossession)
        {
            if (team[i].getHasBall())
                hasPossession = true;
        }
        return hasPossession;
    }
    public bool NoPossession(List<FootBallPlayer> teamA,List<FootBallPlayer> teamB)
    {
        return !hasPossession(teamA) && !hasPossession(teamB);
    }
}
