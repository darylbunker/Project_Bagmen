using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	[Header("Type of weapon")]
	public bool shotgun;
	public bool boltOrPumpAction; 
	public bool semiAutomatic;

	[Header("Firemode")]
	public bool semi;
	public bool auto;

	[Header("Random muzzle effects")]
	public Sprite[] randomMuzzleSideSprite;
	public Sprite[] randomMuzzleSprite;
	
	[Header("Muzzle positions")]
	public GameObject muzzleFlash;
	public GameObject sideMuzzle;
	public GameObject topMuzzle;

	[Header("Light")]
	public GameObject lightFlash;
	[Range(1f,4f)]
	public float lightIntensity;
	[Range(0f,15f)]
	public float lightRange;

	[Header("Particle systems")]
	public ParticleSystem shotgunSparks;
	public ParticleSystem gunSmoke;
	public ParticleSystem heatParticles;

	[Header("Casing prefab")]
	public Transform casing;			
	public Transform casingSpawnPoint; 	

	[Header("Audiosources")] 
	public AudioSource shootSound;		
	public AudioSource reloadSound;
	public AudioSource dryFireSound;
	public AudioSource removeMagSound;
	public AudioSource insertMagSound;
	
	[Header("Reload Options")]

	[Header("Customizable options")]
	public float boltOrPumpDelay; 									
	public float reloadDuration;		

	[Header("Clip")]
	public float clipSize;				
	public float bulletsLeft;			
	bool outOfAmmo;
    [SerializeField]
    private GameObject bulletPrefab;						

	[Header("Firerate and power")]
	public float fireRate;				
	float lastFired;
	public float shootDistance;			
	public float bulletPower;			


	//Makes sure no muzzleflashes are showing at start
	void Start () {
		
		lightFlash.GetComponent<Light> ().intensity = lightIntensity;
		lightFlash.GetComponent<Light> ().range = lightRange;
		
		bulletsLeft = clipSize;
		
		lightFlash.GetComponent<Light> ().enabled = false;			
		muzzleFlash.GetComponent<Renderer> ().enabled = false;
		sideMuzzle.GetComponent<Renderer>().enabled = false;
		topMuzzle.GetComponent<Renderer>().enabled = false;
	}

	//Instantiate casing after x amount of seconds
	IEnumerator CasingEject () {								

		yield return new WaitForSeconds (boltOrPumpDelay);	

		//Set casing to same rotation as spawnpoint
		casing.transform.rotation = casingSpawnPoint.transform.rotation;
		//Spawn casing
		Instantiate (casing, casingSpawnPoint.transform.position, 
		             casingSpawnPoint.transform.rotation);

		if (boltOrPumpAction == true) {									
			reloadSound.Play ();
		}
	}

	//Picks a random muzzleflash sprite and plays particles 
	IEnumerator Shoot () {	

		muzzleFlash.GetComponent<SpriteRenderer>().sprite = randomMuzzleSprite
			[Random.Range(0, randomMuzzleSprite.Length)];
		sideMuzzle.GetComponent<SpriteRenderer> ().sprite = randomMuzzleSideSprite
			[Random.Range (0, randomMuzzleSideSprite.Length)];
		topMuzzle.GetComponent<SpriteRenderer> ().sprite = randomMuzzleSideSprite 
			[Random.Range (0, randomMuzzleSideSprite.Length)];

		muzzleFlash.GetComponent<SpriteRenderer>().enabled = true;		
		sideMuzzle.GetComponent<SpriteRenderer>().enabled = true;
		topMuzzle.GetComponent<SpriteRenderer>().enabled = true;

		lightFlash.GetComponent<Light> ().enabled = true;

		gunSmoke.Play ();
		heatParticles.Play ();

		shootSound.Play ();

        if (shotgun == false)
        {
            GameObject tempBullet = Instantiate(bulletPrefab, this.gameObject.transform.parent.transform.position, this.gameObject.transform.parent.transform.rotation) as GameObject;
            tempBullet.transform.parent = this.gameObject.transform.parent.gameObject.transform;
            tempBullet.gameObject.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            //Debug.Break();
        }	

        yield return new WaitForSeconds (.01f);

		lightFlash.GetComponent<Light> ().enabled = false;			
		muzzleFlash.GetComponent<SpriteRenderer> ().enabled = false;
		sideMuzzle.GetComponent<SpriteRenderer>().enabled = false;
		topMuzzle.GetComponent<SpriteRenderer>().enabled = false;
	}

	//Reloads for x amount of seconds
	IEnumerator Reload () {
		
		outOfAmmo = true;
		removeMagSound.Play (); 									
		
		yield return new WaitForSeconds (reloadDuration);			
		
		insertMagSound.Play ();										
		bulletsLeft = clipSize;
		outOfAmmo = false;
	}

	void Update () {

        if (gameObject.transform.root.gameObject.name == "player")
        {
            //Audio pitch matches timescale, good for slowmotion effects
            GetComponent<AudioSource>().pitch = Time.timeScale;

            if (bulletsLeft < 1)
            {
                bulletsLeft = 0;
                outOfAmmo = true;
            }

            //Play dryfire audio if out of ammo
            if (Input.GetMouseButtonDown(0) && outOfAmmo == true)
            {
                dryFireSound.Play();
            }

            if (Input.GetMouseButton(0) && auto == true)
            {
                if (Time.time - lastFired > 1 / fireRate && outOfAmmo == false)
                {
                    lastFired = Time.time;

                    StartCoroutine(Shoot());
                    StartCoroutine(CasingEject());

                    bulletsLeft -= 1;

                    /*RaycastHit hit;
                    Ray bullet = new Ray (transform.position, transform.right);
                    if (Physics.Raycast (bullet, out hit, shootDistance)) {

                        //Detect raycast hit here
                        hit.rigidbody.AddForce(bullet.direction * bulletPower);
                    }*/

                    if (shotgun == true)
                    {
                        shotgunSparks.Play();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && semi == true)
            {
                if (Time.time - lastFired > 1 / fireRate && outOfAmmo == false)
                {
                    lastFired = Time.time;

                    StartCoroutine(Shoot());
                    StartCoroutine(CasingEject());

                    bulletsLeft -= 1;

                    /*RaycastHit hit;
                    Ray bullet = new Ray (transform.position, transform.right);
                    if (Physics.Raycast (bullet, out hit, shootDistance)) {

                        //Detect raycast hit here
                        hit.rigidbody.AddForce(bullet.direction * bulletPower);
                    }*/

                    if (shotgun == true)
                    {
                        shotgunSparks.Play();
                    }
                }
            }
        }
	
		//Reload when R is pressed
		/*if (Input.GetKeyDown (KeyCode.R)) {						
			StartCoroutine (Reload ());
		}*/

		//Press Q to toggle between semi and auto mode
		/*if (semiAutomatic == true && auto == true && 			
		    		Input.GetKeyDown (KeyCode.Q)) {

			auto = false;
			semi = true;
			//(Optional)Play switching audio here

		} else if (semiAutomatic == true && semi == true && 	
		           Input.GetKeyDown(KeyCode.Q)) {

			auto = true;
			semi = false;
			//(Optional)Play switching audio here
	}*/}
}