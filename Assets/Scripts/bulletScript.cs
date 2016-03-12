using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {





    void Start()
    {

        

    }


    void OnTriggerEnter (Collider hit)
    {

        if (hit.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }

    }


    void OnCollisionEnter (Collision hit)
    {

        if (hit.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
        else if (hit.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
            GameObject.Find("player").GetComponent<playerControls>().Hit();
        }

    }


    void FixedUpdate()
    {

        if (gameObject.name == "shotgunBullet(Clone)" || gameObject.name == "automaticBullet(Clone)")
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 200.0f);
        else
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * 200.0f);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.0f, gameObject.transform.position.z);
        }

    }
	
	
	void Update ()
	{
	
		
	
	}

}
