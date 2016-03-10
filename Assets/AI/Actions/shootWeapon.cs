using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class shootWeapon : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        
        GameObject parentEnemy = ai.Body.transform.root.gameObject;

        foreach (GameObject guns in GameObject.FindGameObjectsWithTag("EnemyFist"))
        {
            if (guns.name == "EnemyRightHandPos")
            {
                if (guns.transform.root.gameObject == parentEnemy)
                {
                    guns.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<GunScript>().EnemyShoot();
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