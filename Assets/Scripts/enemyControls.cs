using UnityEngine;
using System.Collections;
using RAIN.Core;

public class enemyControls : MonoBehaviour {

    private AIRig enemyHealth;
    public GameObject KO_Collider;
    [SerializeField]
    private Material deadMat;
    [SerializeField]
    private GameObject applyMat;
    private GameObject player;
    public bool enemyDown = false;

    void Start ()
	{

        enemyHealth = gameObject.GetComponentInChildren<AIRig>();
        player = GameObject.Find("player");

    }


    private IEnumerator DownForTheCount ()
    {

        yield return new WaitForSeconds(0.5f);
        enemyDown = true;
        KO_Collider.GetComponent<BoxCollider>().enabled = true;

    }


    void OnTriggerEnter (Collider hit)
    {

        if (hit.gameObject.name == "RightHandPos")
        {
            if (enemyHealth.AI.WorkingMemory.GetItem<bool>("ko") == false)
            {
                StartCoroutine(DownForTheCount());
                enemyHealth.AI.WorkingMemory.SetItem<bool>("ko", true);
                enemyHealth.AI.WorkingMemory.SetItem<bool>("inPursuit", false);
                enemyHealth.AI.WorkingMemory.SetItem<GameObject>("playerCharacter", player);
            }
            else if (enemyDown == true)
            {
                enemyHealth.AI.WorkingMemory.SetItem<int>("health", 0);
                enemyHealth.AI.WorkingMemory.SetItem<bool>("ko", false);
                applyMat.gameObject.GetComponent<Renderer>().material = deadMat;
            }
        }
        else if (hit.gameObject.tag == "Melee")
        {
            if (hit.gameObject.transform.root.gameObject.name != "enemy_v2")
            {
                if (enemyHealth.AI.WorkingMemory.GetItem<bool>("ko") == false)
                {
                    StartCoroutine(DownForTheCount());
                    enemyHealth.AI.WorkingMemory.SetItem<bool>("ko", true);
                    enemyHealth.AI.WorkingMemory.SetItem<bool>("inPursuit", false);
                    enemyHealth.AI.WorkingMemory.SetItem<GameObject>("playerCharacter", player);
                }
                else if (enemyDown == true)
                {
                    enemyHealth.AI.WorkingMemory.SetItem<int>("health", 0);
                    enemyHealth.AI.WorkingMemory.SetItem<bool>("ko", false);
                    applyMat.gameObject.GetComponent<Renderer>().material = deadMat;
                }
            }
        }

    }
	
	
	void Update ()
	{
	
		
	
	}

}
