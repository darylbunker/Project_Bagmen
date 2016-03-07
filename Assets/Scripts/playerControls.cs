using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using RAIN.Core;
using RAIN.Entities;

public class playerControls : MonoBehaviour {


    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private GameObject KO_Collider;
    [SerializeField] private Material deadMat;
    [SerializeField] private GameObject applyMat;
    private string playerState = "";
    private GameObject cursor;
    private GameObject rightHand;
    private GameObject weapon;
    private GameObject selectedWeapon;
    [SerializeField] private GameObject shotgunFX;
    [SerializeField] private GameObject handgunFX;
    [SerializeField] private GameObject automaticFX;


    void Start()
    {

        cursor = GameObject.FindWithTag("Cursor");
        rightHand = GameObject.Find("RightHandPos");
        rightHand.gameObject.GetComponent<BoxCollider>().enabled = false;

    }


    public void ResetDemo ()
    {

        SceneManager.LoadScene("demo");

    }


    public void Hit ()
    {

        StopAllCoroutines();
        playerState = "dead";
        applyMat.gameObject.GetComponent<Renderer>().material = deadMat;
        gameObject.GetComponent<Animator>().SetInteger("hit", 2);

    }


    public IEnumerator Stunned ()
    {

        gameObject.GetComponent<Animator>().SetInteger("hit", 1);
        StartCoroutine(RecoverFromStun());
        
        yield return new WaitForSeconds(1.0f);
        playerState = "stunned";
        KO_Collider.GetComponent<BoxCollider>().enabled = true;

        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (enemies.name == "enemy_v2")
            {
                AIRig enemyRig = enemies.GetComponentInChildren<AIRig>();
                enemyRig.AI.WorkingMemory.SetItem<bool>("playerDown", true);
            }
        }

    }


    private IEnumerator RecoverFromStun ()
    {

        yield return new WaitForSeconds(4.0f);

        if (playerState == "stunned")
        {
            gameObject.GetComponent<Animator>().SetInteger("hit", 0);

            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (enemies.name == "enemy_v2")
                {
                    AIRig enemyRig = enemies.GetComponentInChildren<AIRig>();
                    enemyRig.AI.WorkingMemory.SetItem<bool>("playerDown", false);
                }
            }

            //yield return new WaitForSeconds(2.5f);
            playerState = "";
            KO_Collider.GetComponent<BoxCollider>().enabled = false;
        }

    }


    private IEnumerator StopAttack(float time)
    {

        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Animator>().SetInteger("attack", 0);
        rightHand.GetComponent<BoxCollider>().enabled = false;

        if (selectedWeapon != null && selectedWeapon.gameObject.tag == "Melee")
            selectedWeapon.gameObject.GetComponent<MeshCollider>().enabled = false;
    }


    private IEnumerator SetWeapon (string type, GameObject pickUp)
    {

        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Animator>().SetBool(type, true);

        if (type == "melee")
        {
            if (rightHand != null)
            {
                pickUp.gameObject.transform.parent = rightHand.transform;
                pickUp.gameObject.transform.localPosition = Vector3.zero;

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
        }
        else if (type == "fireArm_Heavy" || type == "fireArm_Lite")
        {
            if (rightHand != null)
            {
                pickUp.gameObject.transform.parent = rightHand.transform;
                pickUp.gameObject.transform.localPosition = Vector3.zero;

                if (pickUp.name == "m16")
                {
                    pickUp.gameObject.transform.localEulerAngles = new Vector3(270.0f, 0.0f, 90.0f);
                    pickUp.gameObject.transform.localPosition = new Vector3(0.15f, -0.25f, 0.0f);
                }
                else if (pickUp.name == "shotgun")
                {
                    pickUp.gameObject.transform.localEulerAngles = new Vector3(180.0f, 0.0f, 90.0f);
                    pickUp.gameObject.transform.localPosition = new Vector3(-0.08f, 0.25f, 0.0f);
                }
                else if (pickUp.name == "m1911")
                {
                    pickUp.gameObject.transform.localEulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
                    pickUp.gameObject.transform.localPosition = new Vector3(0.0f, 0.12f, 0.0f);
                }
                else if (pickUp.name == "revolver")
                {
                    pickUp.gameObject.transform.localEulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
                    pickUp.gameObject.transform.localPosition = new Vector3(0.0f, 0.015f, 0.0f);
                }
            }
        }

    }


    private void DetermineWeapon (GameObject item, bool add)
    {

        if (add == true)
        {
            if (weapon == null)
                weapon = item;
            else if (weapon == item.gameObject)
                return;
            else
            {
                float dist1 = Vector3.Distance(gameObject.transform.position, weapon.transform.position);
                float dist2 = Vector3.Distance(gameObject.transform.position, item.transform.position);

                if (dist2 < dist1)
                    weapon = item;
            }
        }
        else
        {
            if (item == weapon)
            {
                weapon = null;
            }
        }

    }


    private void FireWeapon ()
    {

        GameObject shootingPos = selectedWeapon.gameObject.transform.FindChild("effectPos").gameObject;

        if (selectedWeapon.name == "m16")
        {
            GameObject projectile = Instantiate(automaticFX, shootingPos.transform.position, shootingPos.transform.rotation) as GameObject;
            projectile.transform.parent = shootingPos.gameObject.transform;
            //projectile.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else if (selectedWeapon.name == "shotgun")
        {
            GameObject projectile = Instantiate(shotgunFX, shootingPos.transform.position, shootingPos.transform.rotation) as GameObject;
            projectile.transform.parent = shootingPos.gameObject.transform;
        }
        else if (selectedWeapon.name == "m1911")
        {
            GameObject projectile = Instantiate(handgunFX, shootingPos.transform.position, shootingPos.transform.rotation) as GameObject;
            projectile.transform.parent = shootingPos.gameObject.transform;
        }
        else if (selectedWeapon.name == "revolver")
        {
            GameObject projectile = Instantiate(handgunFX, shootingPos.transform.position, shootingPos.transform.rotation) as GameObject;
            projectile.transform.parent = shootingPos.gameObject.transform;
        }

    }


    void OnTriggerEnter(Collider hit)
    {

        if (hit.tag == "Melee" || hit.tag == "FireArm")
        {
            DetermineWeapon(hit.gameObject, true);
        }
        else if (hit.name == "EnemyRightHandPos")
        {
            if (playerState == "")
            {
                StartCoroutine(Stunned());
            }
            else if (playerState == "stunned")
            {
                //playerState = "dead";
                Hit();

                EntityRig playerRig = gameObject.GetComponentInChildren<EntityRig>();
                playerRig.Entity.IsActive = false;

                foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (enemies.name == "enemy_v2")
                    {
                        AIRig enemyRig = enemies.GetComponentInChildren<AIRig>();
                        enemyRig.AI.WorkingMemory.SetItem<bool>("playerDead", true);
                        enemyRig.AI.WorkingMemory.SetItem<bool>("inPursuit", false);
                    }
                }
            }
        }

    }


    void OnTriggerStay (Collider item)
    {

        if (item.tag == "Melee" || item.tag == "FireArm")
        {
            DetermineWeapon(item.gameObject, true);
        }

    }


    void OnTriggerExit (Collider item)
    {

        if (item.tag == "Melee" || item.tag == "FireArm")
        {
            DetermineWeapon(item.gameObject, false);
        }

    }


    void FixedUpdate ()
    {

        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");

        if (playerState != "dead" && playerState != "stunned")
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Lerp(0, xSpeed * movementSpeed, 1.5f), 0.0f, Mathf.Lerp(0, ySpeed * movementSpeed, 1.5f));

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(gameObject.GetComponent<Rigidbody>().velocity, 1.5f * movementSpeed);
        }
        else
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, 10.0f, gameObject.transform.position.z);

        cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(cursor.transform.position.x, 0.0f, cursor.transform.position.z);

        if (playerState != "dead" && playerState != "stunned")
        {
            gameObject.transform.LookAt(cursor.transform.position);
        }

        if (xSpeed != 0 || ySpeed != 0)
        {
            if (ySpeed > 0)
            {
                if ((gameObject.transform.localEulerAngles.y >= 345.0f) || (gameObject.transform.localEulerAngles.y <= 15.0f))
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else if (gameObject.transform.localEulerAngles.y >= 165.0f && gameObject.transform.localEulerAngles.y <= 195.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else
                {
                    if (gameObject.transform.localEulerAngles.y <= 180.0f)
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", 2);
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", -2);
                    }
                }
            }
            else if (ySpeed < 0)
            {
                if ((gameObject.transform.localEulerAngles.y >= 345.0f) || (gameObject.transform.localEulerAngles.y <= 15.0f))
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else if (gameObject.transform.localEulerAngles.y >= 165.0f && gameObject.transform.localEulerAngles.y <= 195.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else
                {
                    if (gameObject.transform.localEulerAngles.y <= 180.0f)
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", 2);
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", -2);
                    }
                }
            }
            else if (xSpeed > 0)
            {
                if (gameObject.transform.localEulerAngles.y >= 75.0f && gameObject.transform.localEulerAngles.y <= 105.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else if (gameObject.transform.localEulerAngles.y >= 255.0f && gameObject.transform.localEulerAngles.y <= 285.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else
                {
                    if (gameObject.transform.localEulerAngles.y <= 90.0f || gameObject.transform.localEulerAngles.y >= 270.0f)
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", 2);
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", -2);
                    }
                }
            }
            else if (xSpeed < 0)
            {
                if (gameObject.transform.localEulerAngles.y >= 75.0f && gameObject.transform.localEulerAngles.y <= 105.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else if (gameObject.transform.localEulerAngles.y >= 255.0f && gameObject.transform.localEulerAngles.y <= 285.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else
                {
                    if (gameObject.transform.localEulerAngles.y <= 90.0f || gameObject.transform.localEulerAngles.y >= 270.0f)
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", 2);
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetInteger("speed", -2);
                    }
                }
            }
        }
        else if (xSpeed == 0 && ySpeed == 0)
        {
            gameObject.GetComponent<Animator>().SetInteger("speed", 0);
        }

    }
	
	
	void Update () 
	{

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedWeapon == null)
            {
                rightHand.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<Animator>().SetInteger("attack", 1);
                StartCoroutine(StopAttack(0.5f));
            }
            else
            {
                if (gameObject.GetComponent<Animator>().GetBool("fireArm_Heavy") == true || gameObject.GetComponent<Animator>().GetBool("fireArm_Lite") == true)
                {
                    //shoot projectile
                    FireWeapon();
                }
                else if (gameObject.GetComponent<Animator>().GetBool("melee") == true)
                {
                    if (selectedWeapon.name != "knuckle")
                    {
                        selectedWeapon.gameObject.GetComponent<MeshCollider>().enabled = true;
                        gameObject.GetComponent<Animator>().SetInteger("attack", 2);
                        StartCoroutine(StopAttack(0.5f));
                    }
                    else
                    {
                        rightHand.GetComponent<BoxCollider>().enabled = true;
                        gameObject.GetComponent<Animator>().SetInteger("attack", 1);
                        StartCoroutine(StopAttack(0.15f));
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rightHand.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<Animator>().SetInteger("attack", 2);
            StartCoroutine(StopAttack(0.5f));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedWeapon == false)
            {
                if (weapon != null)
                {
                    gameObject.GetComponent<Animator>().SetBool("hasWeapon", true);

                    if (weapon.tag == "Melee")
                    {
                        StartCoroutine(SetWeapon("melee", weapon.gameObject));
                        weapon.GetComponent<MeshCollider>().enabled = false;
                    }
                    else
                    {
                        if (weapon.name == "m16" || weapon.name == "shotgun")
                            StartCoroutine(SetWeapon("fireArm_Heavy", weapon.gameObject));
                        else
                            StartCoroutine(SetWeapon("fireArm_Lite", weapon.gameObject));

                        weapon.GetComponent<BoxCollider>().enabled = false;
                    }

                    selectedWeapon = weapon;
                    weapon = null;
                }
            }
            else
            {
                if (weapon != null)
                {
                    GameObject newWeapon = weapon.gameObject;

                    if (selectedWeapon.tag == "Melee")
                    {
                        gameObject.GetComponent<Animator>().SetBool("hasWeapon", false);
                        gameObject.GetComponent<Animator>().SetBool("melee", false);
                        gameObject.GetComponent<Animator>().SetBool("fireArm_Heavy", false);
                        gameObject.GetComponent<Animator>().SetBool("fireArm_Lite", false);
                        selectedWeapon.gameObject.transform.parent = null;
                        selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.25f, selectedWeapon.transform.position.z);
                        selectedWeapon.gameObject.transform.eulerAngles = new Vector3(selectedWeapon.transform.eulerAngles.x, selectedWeapon.transform.eulerAngles.y, 90.0f);
                        selectedWeapon.gameObject.GetComponent<MeshCollider>().enabled = true;
                        selectedWeapon = null;
                    }
                    else if (selectedWeapon.tag == "FireArm")
                    {
                        gameObject.GetComponent<Animator>().SetBool("hasWeapon", false);
                        gameObject.GetComponent<Animator>().SetBool("melee", false);
                        gameObject.GetComponent<Animator>().SetBool("fireArm_Heavy", false);
                        gameObject.GetComponent<Animator>().SetBool("fireArm_Lite", false);
                        selectedWeapon.gameObject.transform.parent = null;

                        if (selectedWeapon.name == "m16")
                        {
                            selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.0f, selectedWeapon.transform.position.z);
                            selectedWeapon.gameObject.transform.eulerAngles = new Vector3(0.0f, selectedWeapon.transform.eulerAngles.y, 270.0f);
                        }
                        else if (selectedWeapon.name == "m1911" || selectedWeapon.name == "revolver")
                        {
                            selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.03f, selectedWeapon.transform.position.z);
                            selectedWeapon.gameObject.transform.eulerAngles = new Vector3(0.0f, selectedWeapon.transform.eulerAngles.y, 90.0f);
                        }
                        else if (selectedWeapon.name == "shotgun")
                        {
                            selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.04f, selectedWeapon.transform.position.z);
                            selectedWeapon.gameObject.transform.eulerAngles = new Vector3(90.0f, selectedWeapon.transform.eulerAngles.y, 0.0f);
                        }

                        selectedWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
                        selectedWeapon = null;
                    }

                    gameObject.GetComponent<Animator>().SetBool("hasWeapon", true);

                    if (newWeapon.tag == "Melee")
                    {
                        StartCoroutine(SetWeapon("melee", newWeapon.gameObject));
                        newWeapon.GetComponent<MeshCollider>().enabled = false;
                    }
                    else
                    {
                        if (newWeapon.name == "m16" || newWeapon.name == "shotgun")
                            StartCoroutine(SetWeapon("fireArm_Heavy", newWeapon.gameObject));
                        else
                            StartCoroutine(SetWeapon("fireArm_Lite", newWeapon.gameObject));

                        newWeapon.GetComponent<BoxCollider>().enabled = false;
                    }

                    selectedWeapon = newWeapon;
                    weapon = null;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (selectedWeapon != null)
            {
                if (selectedWeapon.tag == "Melee")
                {
                    selectedWeapon.gameObject.transform.parent = null;
                    selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.25f, selectedWeapon.transform.position.z);
                    selectedWeapon.gameObject.transform.localEulerAngles = new Vector3(0.0f, selectedWeapon.transform.localEulerAngles.y, 270.0f);
                    selectedWeapon.gameObject.GetComponent<MeshCollider>().enabled = true;
                    selectedWeapon = null;
                    gameObject.GetComponent<Animator>().SetBool("hasWeapon", false);
                    gameObject.GetComponent<Animator>().SetBool("melee", false);
                    gameObject.GetComponent<Animator>().SetBool("fireArm_Heavy", false);
                    gameObject.GetComponent<Animator>().SetBool("fireArm_Lite", false);
                }
                else if (selectedWeapon.tag == "FireArm")
                {
                    selectedWeapon.gameObject.transform.parent = null;
                    if (selectedWeapon.name == "m16")
                    {
                        selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.0f, selectedWeapon.transform.position.z);
                        selectedWeapon.gameObject.transform.eulerAngles = new Vector3(0.0f, selectedWeapon.transform.eulerAngles.y, 270.0f);
                    }
                    else if (selectedWeapon.name == "m1911" || selectedWeapon.name == "revolver")
                    {
                        selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.03f, selectedWeapon.transform.position.z);
                        selectedWeapon.gameObject.transform.eulerAngles = new Vector3(0.0f, selectedWeapon.transform.eulerAngles.y, 90.0f);
                    }
                    else if (selectedWeapon.name == "shotgun")
                    {
                        selectedWeapon.gameObject.transform.position = new Vector3(selectedWeapon.transform.position.x, 0.04f, selectedWeapon.transform.position.z);
                        selectedWeapon.gameObject.transform.eulerAngles = new Vector3(90.0f, selectedWeapon.transform.eulerAngles.y, 0.0f);
                    }
                    selectedWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
                    selectedWeapon = null;
                    gameObject.GetComponent<Animator>().SetBool("hasWeapon", false);
                    gameObject.GetComponent<Animator>().SetBool("melee", false);
                    gameObject.GetComponent<Animator>().SetBool("fireArm_Heavy", false);
                    gameObject.GetComponent<Animator>().SetBool("fireArm_Lite", false);
                }
            }
        }

    }
}
