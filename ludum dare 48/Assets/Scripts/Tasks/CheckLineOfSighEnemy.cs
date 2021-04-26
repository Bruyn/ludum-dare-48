using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckLineOfSighEnemy : Action
{
    public SharedGameObject enemy;
    public SharedVector3 targetPostion;

    public override TaskStatus OnUpdate()
    {
        if (!LineOfSight(enemy.Value))
        {
            targetPostion.Value = enemy.Value.transform.position;
            enemy.Value = null;
        }

        return TaskStatus.Success;
    }

    private bool LineOfSight(GameObject targetObject)
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, targetObject.transform.position, out hit,
            LayerMask.GetMask("Wall", "Player")))
        {
            if (hit.transform.IsChildOf(targetObject.transform) || targetObject.transform.IsChildOf(hit.transform))
            {
                return true;
            }
        }

        return false;
    }
}