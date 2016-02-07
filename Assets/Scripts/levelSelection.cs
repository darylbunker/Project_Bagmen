using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour {


    private int numOfLevels = 1;
    [SerializeField]
    private GameObject[] levelImages;
    private int currentLevelSelected = 1;
    private int[] levelRangeNums = new int[4];

    private Vector3 firstLevelPos = new Vector3(100.0f, -150.0f, 0.0f);
    private Vector3 secondLevelPos_Right = new Vector3(250.0f, -150.0f, 0.0f);
    private Vector3 secondLevelPos_Left = new Vector3(-50.0f, -150.0f, 0.0f);
    private Vector3 thirdLevelPos_Right = new Vector3(350.0f, -150.0f, 0.0f);
    private Vector3 thirdLevelPos_Left = new Vector3(-150.0f, -150.0f, 0.0f);

    private Vector3 firstLevelScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 secondLevelScale = new Vector3(0.85f, 0.85f, 1.0f);
    private Vector3 thirdLevelScale = new Vector3(0.7f, 0.7f, 1.0f);

    private Color firstLevelShade = new Color(1, 1, 1);
    private Color secondLevelShade = new Color(0.75f, 0.75f, 0.75f);
    private Color thirdLevelShade = new Color(0.5f, 0.5f, 0.5f);


    void Start ()
    {

        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("LevelsUnlocked"))
            numOfLevels = PlayerPrefs.GetInt("LevelsUnlocked");
        else
            PlayerPrefs.SetInt("LevelsUnlocked", numOfLevels);

        for (int i = 0; i < numOfLevels; i++)
        {
            levelImages[i].SetActive(true);
        }

        SetLevelRotation();

    }


    private void SetLevelRotation ()
    {

        for (int i = 0; i < numOfLevels; i++)
        {
            levelImages[i].transform.SetSiblingIndex(i);
        }

        if (numOfLevels > 1)
        {
            if (numOfLevels == 2)
            {
                if (currentLevelSelected == 1)
                {
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;

                    levelImages[0].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = secondLevelScale;

                    levelImages[0].GetComponent<Image>().color = firstLevelShade;
                    levelImages[1].GetComponent<Image>().color = secondLevelShade;

                    levelImages[0].transform.SetSiblingIndex(1);
                    levelImages[1].transform.SetSiblingIndex(0);
                }
                else
                {
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;

                    levelImages[0].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = firstLevelScale;

                    levelImages[0].GetComponent<Image>().color = secondLevelShade;
                    levelImages[1].GetComponent<Image>().color = firstLevelShade;

                    levelImages[0].transform.SetSiblingIndex(0);
                    levelImages[1].transform.SetSiblingIndex(1);
                }
            }
            else if (numOfLevels == 3)
            {
                if (currentLevelSelected == 1)
                {
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Right;

                    levelImages[0].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[2].GetComponent<RectTransform>().localScale = thirdLevelScale;

                    levelImages[0].GetComponent<Image>().color = firstLevelShade;
                    levelImages[1].GetComponent<Image>().color = secondLevelShade;
                    levelImages[2].GetComponent<Image>().color = thirdLevelShade;

                    levelImages[0].transform.SetSiblingIndex(2);
                    levelImages[1].transform.SetSiblingIndex(1);
                    levelImages[2].transform.SetSiblingIndex(0);
                }
                else if (currentLevelSelected == 2)
                {
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;

                    levelImages[1].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[2].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[0].GetComponent<RectTransform>().localScale = secondLevelScale;

                    levelImages[1].GetComponent<Image>().color = firstLevelShade;
                    levelImages[2].GetComponent<Image>().color = secondLevelShade;
                    levelImages[0].GetComponent<Image>().color = secondLevelShade;

                    levelImages[0].transform.SetSiblingIndex(1);
                    levelImages[1].transform.SetSiblingIndex(2);
                    levelImages[2].transform.SetSiblingIndex(0);
                }
                else if (currentLevelSelected == 3)
                {
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Left;

                    levelImages[2].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[0].GetComponent<RectTransform>().localScale = thirdLevelScale;

                    levelImages[2].GetComponent<Image>().color = firstLevelShade;
                    levelImages[1].GetComponent<Image>().color = secondLevelShade;
                    levelImages[0].GetComponent<Image>().color = thirdLevelShade;

                    levelImages[0].transform.SetSiblingIndex(0);
                    levelImages[1].transform.SetSiblingIndex(1);
                    levelImages[2].transform.SetSiblingIndex(2);
                }
            }
            else if (numOfLevels == 4)
            {
                if (currentLevelSelected == 1)
                {
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Right;
                    levelImages[3].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Left;

                    levelImages[0].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[2].GetComponent<RectTransform>().localScale = thirdLevelScale;
                    levelImages[3].GetComponent<RectTransform>().localScale = thirdLevelScale;

                    levelImages[0].GetComponent<Image>().color = firstLevelShade;
                    levelImages[1].GetComponent<Image>().color = secondLevelShade;
                    levelImages[2].GetComponent<Image>().color = thirdLevelShade;
                    levelImages[3].GetComponent<Image>().color = thirdLevelShade;

                    levelImages[0].transform.SetSiblingIndex(3);
                    levelImages[1].transform.SetSiblingIndex(2);
                    levelImages[2].transform.SetSiblingIndex(1);
                    levelImages[3].transform.SetSiblingIndex(0);
                }
                else if (currentLevelSelected == 2)
                {
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                    levelImages[3].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Right;
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;

                    levelImages[1].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[2].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[3].GetComponent<RectTransform>().localScale = thirdLevelScale;
                    levelImages[0].GetComponent<RectTransform>().localScale = secondLevelScale;

                    levelImages[1].GetComponent<Image>().color = firstLevelShade;
                    levelImages[2].GetComponent<Image>().color = secondLevelShade;
                    levelImages[3].GetComponent<Image>().color = thirdLevelShade;
                    levelImages[0].GetComponent<Image>().color = secondLevelShade;

                    levelImages[0].transform.SetSiblingIndex(0);
                    levelImages[1].transform.SetSiblingIndex(3);
                    levelImages[2].transform.SetSiblingIndex(2);
                    levelImages[3].transform.SetSiblingIndex(1);
                }
                else if (currentLevelSelected == 3)
                {
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[3].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Left;

                    levelImages[2].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[3].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[0].GetComponent<RectTransform>().localScale = thirdLevelScale;

                    levelImages[2].GetComponent<Image>().color = firstLevelShade;
                    levelImages[3].GetComponent<Image>().color = secondLevelShade;
                    levelImages[1].GetComponent<Image>().color = secondLevelShade;
                    levelImages[0].GetComponent<Image>().color = thirdLevelShade;

                    levelImages[2].transform.SetSiblingIndex(3);
                    levelImages[3].transform.SetSiblingIndex(2);
                    levelImages[1].transform.SetSiblingIndex(1);
                    levelImages[0].transform.SetSiblingIndex(0);
                }
                else if (currentLevelSelected == 4)
                {
                    levelImages[3].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                    levelImages[2].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;
                    levelImages[1].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Left;
                    levelImages[0].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Right;

                    levelImages[3].GetComponent<RectTransform>().localScale = firstLevelScale;
                    levelImages[2].GetComponent<RectTransform>().localScale = secondLevelScale;
                    levelImages[1].GetComponent<RectTransform>().localScale = thirdLevelScale;
                    levelImages[0].GetComponent<RectTransform>().localScale = thirdLevelScale;

                    levelImages[3].GetComponent<Image>().color = firstLevelShade;
                    levelImages[2].GetComponent<Image>().color = secondLevelShade;
                    levelImages[1].GetComponent<Image>().color = thirdLevelShade;
                    levelImages[0].GetComponent<Image>().color = thirdLevelShade;

                    levelImages[3].transform.SetSiblingIndex(3);
                    levelImages[2].transform.SetSiblingIndex(2);
                    levelImages[1].transform.SetSiblingIndex(1);
                    levelImages[0].transform.SetSiblingIndex(0);
                }
            }
            else if (numOfLevels >= 5)
            {
                ShowLevelRange();

                levelImages[currentLevelSelected-1].GetComponent<RectTransform>().anchoredPosition = firstLevelPos;
                levelImages[levelRangeNums[0]-1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Right;
                levelImages[levelRangeNums[1]-1].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Right;
                levelImages[levelRangeNums[2]-1].GetComponent<RectTransform>().anchoredPosition = thirdLevelPos_Left;
                levelImages[levelRangeNums[3]-1].GetComponent<RectTransform>().anchoredPosition = secondLevelPos_Left;

                levelImages[currentLevelSelected-1].GetComponent<RectTransform>().localScale = firstLevelScale;
                levelImages[levelRangeNums[0]-1].GetComponent<RectTransform>().localScale = secondLevelScale;
                levelImages[levelRangeNums[1]-1].GetComponent<RectTransform>().localScale = thirdLevelScale;
                levelImages[levelRangeNums[2]-1].GetComponent<RectTransform>().localScale = thirdLevelScale;
                levelImages[levelRangeNums[3]-1].GetComponent<RectTransform>().localScale = secondLevelScale;

                levelImages[currentLevelSelected-1].GetComponent<Image>().color = firstLevelShade;
                levelImages[levelRangeNums[0]-1].GetComponent<Image>().color = secondLevelShade;
                levelImages[levelRangeNums[1]-1].GetComponent<Image>().color = thirdLevelShade;
                levelImages[levelRangeNums[2]-1].GetComponent<Image>().color = thirdLevelShade;
                levelImages[levelRangeNums[3]-1].GetComponent<Image>().color = secondLevelShade;

                levelImages[currentLevelSelected - 1].transform.SetSiblingIndex(numOfLevels-1);
                levelImages[levelRangeNums[0] - 1].transform.SetSiblingIndex(numOfLevels-2);
                levelImages[levelRangeNums[1] - 1].transform.SetSiblingIndex(numOfLevels-3);
                levelImages[levelRangeNums[2] - 1].transform.SetSiblingIndex(numOfLevels-4);
                levelImages[levelRangeNums[3] - 1].transform.SetSiblingIndex(numOfLevels-3);
            }
        }

    }


    private void ShowLevelRange ()
    {

        levelRangeNums[0] = currentLevelSelected + 1;
        if (levelRangeNums[0] > numOfLevels)
            levelRangeNums[0] = levelRangeNums[0] % numOfLevels;

        levelRangeNums[1] = currentLevelSelected + 2;
        if (levelRangeNums[1] > numOfLevels)
            levelRangeNums[1] = levelRangeNums[1] % numOfLevels;

        if (currentLevelSelected == 1)
        {
            levelRangeNums[2] = numOfLevels - 1;
            levelRangeNums[3] = numOfLevels;
        }
        else if (currentLevelSelected == 2)
        {
            levelRangeNums[2] = numOfLevels;
            levelRangeNums[3] = currentLevelSelected - 1;
        }
        else
        {
            levelRangeNums[2] = currentLevelSelected - 2;

            levelRangeNums[3] = currentLevelSelected - 1;

        }

    }


    public void LoadLevel (string level)
    {

        SceneManager.LoadScene(level);

    }


    public void AddLevels ()
    {

        numOfLevels++;
        PlayerPrefs.SetInt("LevelsUnlocked", numOfLevels);

        for (int i = 0; i < numOfLevels; i++)
        {
            levelImages[i].SetActive(true);
        }

        SetLevelRotation();

    }
	
	
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numOfLevels < levelImages.Length)
                AddLevels();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLevelSelected--;

            if (currentLevelSelected < 1)
                currentLevelSelected = numOfLevels;

            SetLevelRotation();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLevelSelected++;

            if (currentLevelSelected > numOfLevels)
                currentLevelSelected = 1;

            SetLevelRotation();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(levelImages[currentLevelSelected-1].name);
        }

    }
}
