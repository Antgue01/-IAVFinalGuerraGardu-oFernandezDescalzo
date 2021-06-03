using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IAFutbol")]
[TaskDescription("Comprueba si el jugador puede tirar")]
public class CanShoot : Conditional
{
    private FootBallPlayer player;
    private BoxCollider goal;
    public LayerMask PlayersLayer;
    [SerializeField] const float minimumDistance = 5;
    public override void OnAwake()
    {
        player = GetComponent<FootBallPlayer>();
        if (player)
            goal = player.getGoalZone();
    }
    public override TaskStatus OnUpdate()
    {
        if (goal != null && (goal.transform.position - transform.position).magnitude < minimumDistance)
        {

            Vector3 corner1 = goal.bounds.center - goal.bounds.extents;
            Vector3 corner2 = goal.bounds.center + goal.bounds.extents;
            bool intersects = false;
            int i = 0;
            int layer = 1 << PlayersLayer;
            Vector3 interPoint = new Vector3();
            while (i < 5 && !intersects)
            {
                interPoint = Vector3.Lerp(corner1, corner2, (float).2 * i);
                if (Physics.Raycast(player.transform.position, interPoint - player.transform.position, layer))
                    intersects = true;

                i++;
            }
            if (!intersects)
            {
                Vector3 shootDirection = (Vector3.Lerp(corner1, corner2, Random.Range(0f, 1.000001f)) - player.transform.position);
                player.setShootDirection(shootDirection.normalized);
                return TaskStatus.Success;
            }
            else
            {
                player.setShootDirection(Vector3.zero);
                return TaskStatus.Failure;
            }
        }
        else return TaskStatus.Failure;
    }


}
