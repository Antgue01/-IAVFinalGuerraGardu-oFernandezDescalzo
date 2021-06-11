using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class HiveMind : MonoBehaviour
{
    Vector3[] posDefensas;
    Vector3[] posDelanteros;

    private void Update()
    {

    }

    public void init()
    {
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
            if(defensas.Count >= 2)
            {
                posDefensas[0].x = defensas[0].getLimitAttack();
                posDefensas[defensas.Count - 1].x = defensas[0].getLimitAttack();
            }
        }

        //Posiciones delanteros
        if (delanteros.Count != 0)
        {
            posDelanteros = new Vector3[delanteros.Count];
            float limitDefense = delanteros[0].getLimitDefense();
            Collider campo = GameManager.getInstance().getCampo();
            //Separacion entre delanteros
            float diffY = (campo.bounds.extents.z * 2) / (delanteros.Count + 1);
            float campoMaxZ = campo.bounds.center.z + GameManager.getInstance().getCampo().bounds.extents.z;
            for (int i = 1; i <= delanteros.Count; i++)
            {
                posDelanteros[i - 1] = new Vector3(limitDefense, delanteros[0].transform.position.y, campoMaxZ - (i * diffY));
            }
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
        foreach (FootBallPlayer centro in centros)
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
        foreach (FootBallPlayer delantero in delanteros)
        {
            if (!delantero.getHasBall())
                delantero.spread();
        }

    }

    public void defend(List<FootBallPlayer> delanteros, List<FootBallPlayer> centros, List<FootBallPlayer> defensas, List<FootBallPlayer> enemyPlayers)
    {
        //Delanteros
        for (int i = 0; i < delanteros.Count; i++) delanteros[i].goTo(posDelanteros[i]);

        //Centros
        for(int i=0; i< centros.Count/2; i++)
        {
            centros[i].goTo(new Vector3(centros[0].getLimitDefense(), centros[i].transform.position.y, centros[i].transform.position.z));
        }

        List<FootBallPlayer> otherPlayers = new List<FootBallPlayer>();
        for (int i = centros.Count / 2; i < centros.Count; i++) otherPlayers.Add(centros[i]);
        foreach (FootBallPlayer defensa in defensas) otherPlayers.Add(defensa);

        FootBallPlayer owner = GameManager.getInstance().getBallOwner();
        if(owner != null)
        {
            float minDist;
            Vector3 ballPos = GameManager.getInstance().getBallPosition();
            
            minDist = float.MaxValue;
            FootBallPlayer closest = null;
            foreach (FootBallPlayer player in otherPlayers)
            {
                float dist = Vector3.Distance(ballPos, player.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = player;
                }
            }
            otherPlayers.Remove(closest);
            closest.goTo(ballPos);
        }

        //El resto van a por el jugador mas cercano sin balon y lo cubren
        List<FootBallPlayer> enemies = new List<FootBallPlayer>(enemyPlayers);
        if (owner != null) enemies.Remove(owner);
           
        for(int i=0; i < otherPlayers.Count; i++)
        {
            float min = float.MaxValue;
            FootBallPlayer enemyToCover = null;
            for(int j = 0; j < enemies.Count; j++)
            {
                float distance = Vector3.Distance(enemies[j].transform.position, otherPlayers[i].transform.position);
                if(distance < min)
                {
                    min = distance;
                    enemyToCover = enemies[j];
                }
            }
            otherPlayers[i].coverPlayer(new Vector3(enemyToCover.transform.position.x, enemyToCover.transform.position.y, enemyToCover.transform.position.z));
            enemies.Remove(enemyToCover);
        }
    }
}
