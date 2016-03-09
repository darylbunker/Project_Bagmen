using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {





    void Start()
    {

        

    }


    void FixedUpdate()
    {

        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * 20.0f);

    }
	
	
	void Update ()
	{
	
		
	
	}

}
