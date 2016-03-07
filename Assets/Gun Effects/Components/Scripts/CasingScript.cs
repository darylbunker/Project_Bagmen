
using UnityEngine;
using System.Collections;

public class CasingScript : MonoBehaviour {
	
	bool hitGround;					

	[Header("X force")]
	[Header("Eject speed")]
	public float minimumXForce;					
	public float maximumXForce;

	[Header("Y force")]
	public float minimumYForce;
	public float maximumYForce;

	[Header("Z force")]
	public float minimumZForce;
	public float maximumZForce;

	[Header("Spin")]
	public float rotateSpeed;				

	[Header("Destroy")]
	public float despawnTime;				

	[Header("Ground hit")]
	public string groundTag;				

	[Header("Audio source")]
	public AudioSource shellCasingSound;  	

		
	IEnumerator RemoveCasing () {

		//Deletes the casing after x amount of seconds
		yield return new WaitForSeconds (despawnTime);

		Destroy (gameObject);
	}

	void Start () {

		//Make sure hitground is false at spawn
		hitGround = false;

		//Remove the casing after x amount of time
		StartCoroutine (RemoveCasing ());

		//Random direction the casing will be ejected in
		GetComponent<Rigidbody>().AddRelativeForce  
				(Random.Range (minimumXForce, maximumXForce), //X Axis
	             Random.Range (minimumYForce, maximumYForce), //Y Axis
				 Random.Range (minimumZForce, maximumZForce) //Z Axis
		         * Time.deltaTime * 350); 
	}

	void Update () {

		if (hitGround == false) {
			transform.RotateAround (transform.position, 
			        				transform.up, Time.deltaTime * rotateSpeed);
		}
		//Stop spinning
	}

	void OnCollisionEnter (Collision groundHit)
	{
		//Checks when the casing collides with ground tag
		if(groundHit.gameObject.tag == groundTag)
		{
			hitGround = true;
			shellCasingSound.Play();
	}}
}