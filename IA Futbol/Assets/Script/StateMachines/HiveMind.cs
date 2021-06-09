using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class HiveMind : MonoBehaviour
{
    Vector3[] posDefensas;



    private void Update()
    {

    }

    public void init()
    {
        Debug.Log("hivemind");
        Team MyTeam = Variables.Object(this.gameObject).Get("MyTeam").ConvertTo<Team>();
        Team EnemyTeam = Variables.Object(this.gameObject).Get("EnemyTeam").ConvertTo<Team>();
        List<FootBallPlayer> myTeam = GameManager.getInstance().getTeam(MyTeam);
        List<FootBallPlayer> enemyTeam = GameManager.getInstance().getTeam(EnemyTeam);
        Variables.Object(this.gameObject).Set("MyPlayers", myTeam);
        Variables.Object(this.gameObject).Set("EnemyPlayers", enemyTeam);

        List<FootBallPlayer> delanteros = new List<FootBallPlayer>();
        List<FootBallPlayer> centros = new List<FootBallPlayer>();
        List<FootBallPlayer> defensas = new List<FootBallPlayer>();

        foreach (FootBallPlayer player in myTeam)
        {
            if (player.getMyRol() == Rol.Delantero) delanteros.Add(player);
            else if (player.getMyRol() == Rol.Centro) centros.Add(player);
            else defensas.Add(player);
        }

        Variables.Object(this.gameObject).Set("Delanteros", delanteros);
        Variables.Object(this.gameObject).Set("Centros", centros);
        Variables.Object(this.gameObject).Set("Defensas", defensas);

        //Posiciones defensas
        if (defensas.Count != 0)
        {
            posDefensas = new Vector3[defensas.Count];
            float limitAttack = defensas[0].getLimitAttack();
            float goalX = GameManager.getInstance().getGoalZone(MyTeam).position.x;
            float defX = (limitAttack + goalX) / 2.0f;
            Collider campo = GameManager.getInstance().getCampo();
            //Separacion entre defensas
            float diffY = (campo.bounds.extents.z * 2) / (defensas.Count + 1);
            float campoMaxZ = campo.bounds.center.z + GameManager.getInstance().getCampo().bounds.extents.z;
            for (int i = 1; i <= defensas.Count; i++)
            {
                posDefensas[i - 1] = new Vector3(defX, defensas[0].transform.position.y, campoMaxZ - (i * diffY));
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

        bool hasPossession = false;
        int i = 0;
        while (i < team.Count && !hasPossession)
        {
            if (team[i].getHasBall())
                hasPossession = true;
            i++;
        }
        return hasPossession;
    }
    public bool NoPossession(List<FootBallPlayer> teamA, List<FootBallPlayer> teamB)
    {
        return !hasPossession(teamA) && !hasPossession(teamB);
    }

    public void attack(List<FootBallPlayer> delanteros, List<FootBallPlayer> centros, List<FootBallPlayer> defensas)
    {

        //Defensas
        //Distribuimos los defensas repartiendolos por su zona, cubriendo el mayor area posible preparados para defender
        for (int i = 0; i < defensas.Count; i++)
        {
            if (!defensas[i].getHasBall())
            {
                defensas[i].goTo(posDefensas[i]);
            }
        }
        int numCentrosSinPelota = 0;
        foreach (var centro in centros)
        {
            if (!centro.getHasBall())
            {
                numCentrosSinPelota++;
            }
        }
        Collider campo = GameManager.getInstance().getCampo();
        float diffY = (campo.bounds.extents.z * 2) / ((numCentrosSinPelota / 2) + 1);
        float campoMaxZ = campo.bounds.center.z + GameManager.getInstance().getCampo().bounds.extents.z;

        for (int i = 1; i <= numCentrosSinPelota / 2; i++)
        {
            if (!centros[i - 1].getHasBall())
                centros[i - 1].goTo(new Vector3(centros[0].getLimitDefense(), centros[i - 1].transform.position.y, campoMaxZ - (i * diffY)));
        }
        for (int i = numCentrosSinPelota / 2; i < centros.Count; i++)
        {
            if (!centros[i].getHasBall())
                centros[i].spread();
        }

    }
}
