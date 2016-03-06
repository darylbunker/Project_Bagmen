using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class disableHandCollider : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject parentEnemy = ai.Body.transform.root.gameObject;

        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("EnemyFist"))
        {
            if (enemies.name == "EnemyRightHandPos")
            {
                if (enemies.transform.root.gameObject == parentEnemy)
                {
                    enemies.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}