using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    
    [SerializeField] Team Team;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.getInstance().Goal(Team);
    }
}
