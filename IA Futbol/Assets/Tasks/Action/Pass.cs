using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IAFutbol")]
[TaskDescription("Se la pasa al compañero óptimo")]
public class Pass : Action
{
    private FootBallPlayer player;
    public LayerMask PlayersLayer;
    List<FootBallPlayer> team;
    public override void OnAwake()
    {
        player = GetComponent<FootBallPlayer>();
    }

    public override void OnStart()
    {
        team = player.getTeam();
        for(int i=0; i<team.Count; i++)
        {
            Debug.Log(team[i].getMyTeam());
        }
    }

    public override TaskStatus OnUpdate()
    {
        List<FootBallPlayer> canPass = new List<FootBallPlayer>();
        RaycastHit info;
        foreach (FootBallPlayer canPassPlayer in team)
        {
            if (canPassPlayer != player)
            {
                Vector3 direction = canPassPlayer.transform.position - player.transform.position;
                bool collides = Physics.Raycast(player.transform.position, direction.normalized, out info, direction.magnitude, PlayersLayer);
                Debug.DrawRay(player.transform.position, direction, Color.red, 3);
                
                //si no hay nadie en medio o si lo que hay es alguien de mi equipo
                if(info.collider.GetComponent<FootBallPlayer>() == null) Debug.Log(info.collider.gameObject.name);
                Team myTeam = info.collider.GetComponent<FootBallPlayer>().getMyTeam();
                if (!collides || (collides && myTeam == player.getMyTeam()))
                {
                    canPass.Add(canPassPlayer);
                }
            }
        }
        FootBallPlayer furthest = null;
        float targetDistance = float.MaxValue;
        foreach (FootBallPlayer footBallPlayer in canPass)
        {
            float distance = (footBallPlayer.getGoalZone().transform.position - footBallPlayer.transform.position).magnitude;
            if (distance < targetDistance)
                furthest = footBallPlayer;
            targetDistance = distance;
        }
        player.Pass(furthest);
        return TaskStatus.Success;
    }
}
