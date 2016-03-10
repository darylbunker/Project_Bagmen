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

    }


    void FixedUpdate()
    {

        if (gameObject.name == "shotgunBullet(Clone)" || gameObject.name == "automaticBullet(Clone)")
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 200.0f);
        else
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * 200.0f);

    }
	
	
	void Update ()
	{
	
		
	
	}

}
