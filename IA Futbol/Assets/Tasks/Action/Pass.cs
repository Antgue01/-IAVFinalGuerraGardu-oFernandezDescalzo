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
        team = player.getTeam();
    }
    public override TaskStatus OnUpdate()
    {
        List<FootBallPlayer> canPass = new List<FootBallPlayer>();
        foreach (FootBallPlayer canPassPlayer in team)
        {
            if (canPassPlayer != player)
            {
                int layer = 1 << PlayersLayer;
                Vector3 direction = canPassPlayer.transform.position - player.transform.position;
                RaycastHit info;
                bool collides = Physics.Raycast(player.transform.position, direction, out info, direction.magnitude, layer);
                //si no hay nadie en medio o si lo que hay es alguien de mi equipo
                if (!collides || (collides && info.collider.GetComponent<FootBallPlayer>().getMyTeam() == player.getMyTeam()))
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
