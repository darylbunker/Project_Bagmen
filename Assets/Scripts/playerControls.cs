using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class playerControls : MonoBehaviour {


    [SerializeField] private float movementSpeed = 10.0f;
    private float top, bottom, left, right;
    private string playerState = "";
    private GameObject cursor;
    private GameObject rightHand;
    private GameObject weapon;
    private GameObject selectedWeapon;


    void Start()
    {

        cursor = GameObject.FindWithTag("Cursor");
        rightHand = GameObject.Find("RightHandPos");
        top = (Screen.height / 2) + (Screen.height / 10);
        bottom = (Screen.height / 2) - (Screen.height / 10);
        right = (Screen.width / 2) + (Screen.width / 10);
        left = (Screen.width / 2) - (Screen.width / 10);

    }


    public void ResetDemo ()
    {

        SceneManager.LoadScene("demo");

    }


    public void Hit ()
    {

        playerState = "dead";
        gameObject.GetComponent<Animator>().SetInteger("hit", 2);

    }


    public void Stunned ()
    {

        playerState = "stunned";
        gameObject.GetComponent<Animator>().SetInteger("hit", 1);
        StartCoroutine(RecoverFromStun());

    }


    private IEnumerator RecoverFromStun ()
    {

        yield return new WaitForSeconds(4.0f);
        gameObject.GetComponent<Animator>().SetInteger("hit", 0);
        yield return new WaitForSeconds(2.5f);
        playerState = "";

    }


    private IEnumerator StopAttack(float time)
    {

        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Animator>().SetInteger("attack", 0);

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


    void OnTriggerEnter(Collider hit)
    {

        if (hit.tag == "Melee" || hit.tag == "FireArm")
        {
            DetermineWeapon(hit.gameObject, true);
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
                if ((gameObject.transform.eulerAngles.y >= 345.0f) || (gameObject.transform.eulerAngles.y <= 15.0f))
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else if (gameObject.transform.eulerAngles.y >= 165.0f && gameObject.transform.eulerAngles.y <= 195.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else
                {
                    if (gameObject.transform.eulerAngles.y <= 180.0f)
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
                if ((gameObject.transform.eulerAngles.y >= 345.0f) || (gameObject.transform.eulerAngles.y <= 15.0f))
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else if (gameObject.transform.eulerAngles.y >= 165.0f && gameObject.transform.eulerAngles.y <= 195.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else
                {
                    if (gameObject.transform.eulerAngles.y <= 180.0f)
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
                if (gameObject.transform.eulerAngles.y >= 75.0f && gameObject.transform.eulerAngles.y <= 105.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else if (gameObject.transform.eulerAngles.y >= 255.0f && gameObject.transform.eulerAngles.y <= 285.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else
                {
                    if (gameObject.transform.eulerAngles.y <= 90.0f || gameObject.transform.eulerAngles.y >= 270.0f)
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
                if (gameObject.transform.eulerAngles.y >= 75.0f && gameObject.transform.eulerAngles.y <= 105.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", -1);
                }
                else if (gameObject.transform.eulerAngles.y >= 255.0f && gameObject.transform.eulerAngles.y <= 285.0f)
                {
                    gameObject.GetComponent<Animator>().SetInteger("speed", 1);
                }
                else
                {
                    if (gameObject.transform.eulerAngles.y <= 90.0f || gameObject.transform.eulerAngles.y >= 270.0f)
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
                gameObject.GetComponent<Animator>().SetInteger("attack", 1);
                StartCoroutine(StopAttack(0.15f));
            }
            else
            {
                if (gameObject.GetComponent<Animator>().GetBool("fireArm_Heavy") == true || gameObject.GetComponent<Animator>().GetBool("fireArm_Lite") == true)
                {
                    //shoot projectile
                }
                else if (gameObject.GetComponent<Animator>().GetBool("melee") == true)
                {
                    if (selectedWeapon.name != "knuckle")
                    {
                        gameObject.GetComponent<Animator>().SetInteger("attack", 2);
                        StartCoroutine(StopAttack(0.5f));
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetInteger("attack", 1);
                        StartCoroutine(StopAttack(0.15f));
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
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
