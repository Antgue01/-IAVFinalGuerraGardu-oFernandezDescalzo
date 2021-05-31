using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FootBallPlayer player = collision.gameObject.GetComponent<FootBallPlayer>();
        if (player)
        {
            if (transform.parent != null)
            {
                FootBallPlayer parent = transform.parent.gameObject.GetComponent<FootBallPlayer>();
                if (parent)
                    parent.setHasBall(false);

            }
            player.setHasBall(true);
            transform.SetParent(collision.transform);
        }
    }
}
