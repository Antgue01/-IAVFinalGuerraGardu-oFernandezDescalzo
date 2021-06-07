using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class HiveMind : MonoBehaviour
{
    Vector3[] posDefensas;

    public void init(List<FootBallPlayer> myTeam, List<FootBallPlayer> enemyTeam)
    {
        Variables.Object(this.gameObject).Set("MyPlayers", myTeam);
        Variables.Object(this.gameObject).Set("EnemyPlayers", enemyTeam);

        List<FootBallPlayer> delanteros = new List<FootBallPlayer>();
        List<FootBallPlayer> centros = new List<FootBallPlayer>();
        List<FootBallPlayer> defensas = new List<FootBallPlayer>();

        foreach(FootBallPlayer player in myTeam)
        {
            if(player.getMyRol() == Rol.Delantero) delanteros.Add(player);
            else if(player.getMyRol() == Rol.Centro) centros.Add(player);
            else defensas.Add(player);
        }

        Variables.Object(this.gameObject).Set("Delanteros", delanteros);
        Variables.Object(this.gameObject).Set("Centros", centros);
        Variables.Object(this.gameObject).Set("Defensas", defensas);

        //Posiciones defensas
        if (defensas.Count != 0)
        {
            posDefensas = new Vector3[defensas.Count];
            float defX = defensas[0].getLimitAttack() - ((defensas[0].getLimitAttack() - defensas[0].getGoalZone().transform.position.x) / 2.0f);
            Collider campo = GameManager.getInstance().getCampo();
            //Separacion entre defensas
            float diffY = (campo.bounds.extents.z * 2) / defensas.Count;
            float campoMaxZ = campo.bounds.center.z + GameManager.getInstance().getCampo().bounds.extents.z;
            for (int i = 0; i < defensas.Count; i++)
            {
                posDefensas[i] = new Vector3(defX, defensas[0].transform.position.y, campoMaxZ - (i * diffY));
            }
            posDefensas[0].x = defensas[0].getLimitAttack();
            posDefensas[defensas.Count - 1].x = defensas[0].getLimitAttack();
        }
    }

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
        Debug.Log("hasPossesion");
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

    public void attack(List<FootBallPlayer> delanteros, List<FootBallPlayer> centros, List<FootBallPlayer> defensas)
    {
        Debug.Log("attacvk");
        //Defensas
        //Distribuimos los defensas repartiendolos por su zona, cubriendo el mayor area posible preparados para defender
        for (int i = 0; i < defensas.Count; i++)
        {
            if (!defensas[i].getHasBall())
            {
                defensas[i].goTo(posDefensas[i]);
            }
        }
    }
}
