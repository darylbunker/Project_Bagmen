using UnityEngine;
using System.Collections;
using RAIN.Core;

public class enemyControls : MonoBehaviour {

    AIRig enemyHealth;
    GameObject player;

    void Start ()
	{

        enemyHealth = gameObject.GetComponentInChildren<AIRig>();
        player = GameObject.Find("player");

    }


    void OnTriggerEnter (Collider hit)
    {

        if (hit.gameObject.name == "RightHandPos")
        {
            enemyHealth.AI.WorkingMemory.SetItem<float>("health", 1);
            enemyHealth.AI.WorkingMemory.SetItem<GameObject>("playerCharacter", player);
        }

    }
	
	
	void Update ()
	{
	
		
	
	}

}
