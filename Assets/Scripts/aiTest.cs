using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class aiTest : MonoBehaviour {
	
	
	
	
	
	void Start ()
	{
	
		
	
	}


    public void Reload ()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    public void LoadNew (int choice)
    {

        SceneManager.LoadScene(choice);

    }
	
	
	void Update ()
	{
	
		
	
	}

}
