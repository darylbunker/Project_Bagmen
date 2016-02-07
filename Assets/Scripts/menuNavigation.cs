using UnityEngine;
using System.Collections;

public class menuNavigation : MonoBehaviour {


    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject campaignMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject quitConfirmation;


    void Start ()
    {

        mainMenu.SetActive(true);
        campaignMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitConfirmation.SetActive(false);

	}


    public void ShowCampaign ()
    {

        mainMenu.SetActive(false);
        campaignMenu.SetActive(true);

    }


    public void ShowSettings ()
    {

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

    }


    public void BackToMain ()
    {

        mainMenu.SetActive(true);
        campaignMenu.SetActive(false);
        settingsMenu.SetActive(false);

    }


    public void QuitGameConfirmation ()
    {

        quitConfirmation.SetActive(true);

    }


    public void QuitGame ()
    {

        Application.Quit();

    }


    public void CancelQuit ()
    {

        quitConfirmation.SetActive(false);

    }
	
	
	void Update ()
    {
	


	}
}
