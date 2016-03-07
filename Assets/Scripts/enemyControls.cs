using UnityEngine;
using System.Collections;
using RAIN.Core;

public class enemyControls : MonoBehaviour {

    private AIRig enemyHealth;
    [SerializeField] private GameObject rightHand;
    private GameObject pickUp;
    public GameObject KO_Collider;
    [SerializeField] private Material deadMat;
    [SerializeField] private GameObject applyMat;
    private GameObject player;
    public bool enemyDown = false;
    [SerializeField] private GameObject shotgunFX;
    [SerializeField] private GameObject handgunFX;
    [SerializeField] private GameObject automaticFX;

    void Start ()
	{

        enemyHealth = gameObject.GetComponentInChildren<AIRig>();
        player = GameObject.Find("player");

        if (rightHand.gameObject.transform.childCount > 0)
        {
            pickUp = rightHand.gameObject.transform.GetChild(0).gameObject;
            SetWeapon();
        }

    }


    private void SetWeapon()
    {

        pickUp.gameObject.transform.parent = rightHand.transform;
        pickUp.gameObject.transform.localPosition = Vector3.zero;

        if (pickUp.gameObject.tag == "Melee")
        {
            enemyHealth.AI.WorkingMemory.SetItem<bool>("hasMeleeWeapon", true);

            if (pickUp.name != "butcherKnife" && pickUp.name != "fireAxe" && pickUp.name != "hammer" && pickUp.name != "heavyWrench" && pickUp.name != "machete" && pickUp.name != "miningPick" && pickUp.name != "sledgeHammer")
                pickUp.gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            else
                pickUp.gameObject.transform.localEulerAngles = new Vector3(270.0f, 90.0f, 0.0f);

            if (pickUp.gameObject.name == "knuckle")
            {
                pickUp.gameObject.transform.localPosition = new Vector3(0.115f, 0.05f, 0.0f);
                pickUp.gameObject.transform.localEulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
            }
        }
        else
        {
            if (pickUp.name == "m16")
            {
                enemyHealth.AI.WorkingMemory.SetItem<bool>("hasHeavyFirearm", true);

                pickUp.gameObject.transform.localEulerAngles = new Vector3(270.0f, 0.0f, 90.0f);
                pickUp.gameObject.transform.localPosition = new Vector3(0.15f, -0.25f, 0.0f);
            }
            else if (pickUp.name == "shotgun")
            {
                enemyHealth.AI.WorkingMemory.SetItem<bool>("hasHeavyFirearm", true);

                pickUp.gameObject.transform.localEulerAngles = new Vector3(180.0f, 0.0f, 90.0f);
                pickUp.gameObject.transform.localPosition = new Vector3(-0.08f, 0.25f, 0.0f);
            }
            else if (pickUp.name == "m1911")
            {
                enemyHealth.AI.WorkingMemory.SetItem<bool>("hasLiteFirearm", true);

                pickUp.gameObject.transform.localEulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
                pickUp.gameObject.transform.localPosition = new Vector3(0.0f, 0.12f, 0.0f);
            }
            else if (pickUp.name == "revolver")
            {
                enemyHealth.AI.WorkingMemory.SetItem<bool>("hasLiteFirearm", true);

                pickUp.gameObject.transform.localEulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
                pickUp.gameObject.transform.localPosition = new Vector3(0.0f, 0.015f, 0.0f);
            }
        }

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
