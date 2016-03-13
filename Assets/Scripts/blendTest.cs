using UnityEngine;
using System.Collections;

public class blendTest : MonoBehaviour {
	
	
	
	
	
	void Start ()
	{
	
		
	
	}
	
	
	void Update ()
	{
	
		if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Animator>().SetFloat("walkBlend", 1.0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Animator>().SetFloat("walkBlend", -1.0f);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetFloat("walkBlend", 0.0f);
        }
	
	}

}
