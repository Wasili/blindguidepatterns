using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {
    string lastPlayedLevel;
    int totalUnlockedLevels, currentMenuItem;
    //geef in unity het eerste menu item aan (het menu dat als eerste wordt vertoond)
    public int firstMenuItem = 0;

    //deze array wordt gebruikt om de menu items aan en uit te zetten
    //hierdoor kan het script gebruikt worden voor verschillende scenes
    public GameObject[] menuObjects;

    public Button[] levelSelectButtons;

    public AudioClip buttonPressSound;

    public Image pauseBackgroundImage;
    float pauseScreenFadeSpeed;
    Color pauseBackgroundColor;
    string pauseButtonName = "Pause";

	void Start() 
    {
        //CHEATS
        //ResetLevelProgress();
        //PlayerPrefs.SetInt("LastUnlockedLevel", levelSelectButtons.Length);


        //zet de timescale op 1 zodat het pauze menu niet het hele spel vast laat lopen als er een nieuwe scene wordt geladen vanuit het pauze menu
        Time.timeScale = 1;
        //PlayerPrefs is een ingebouwde functie om een tekst bestand aan te maken zodat er data kan worden opgeslagen
        //Dit betekent dus dat deze data moet worden bijgehouden tijdens het spelen door PlayerPrefs.SetString (bijvoorbeeld) aan te roepen
        lastPlayedLevel = PlayerPrefs.GetString("LastPlayedLevel", "Level1");
        totalUnlockedLevels = PlayerPrefs.GetInt("LastUnlockedLevel", 1);
        AudioListener.pause = false;

        //activeer alleen levels die zijn unlocked
        if (levelSelectButtons != null) 
        {
            for (int i = 0; i < levelSelectButtons.Length; i++)
            {
                levelSelectButtons[i].interactable = (i + 1 <= totalUnlockedLevels);
            }
        }

        currentMenuItem = firstMenuItem;

        if (pauseBackgroundImage != null)
        {
            pauseBackgroundColor = pauseBackgroundImage.color;
            pauseBackgroundColor.a = 0;
            pauseBackgroundImage.color = pauseBackgroundColor;
            pauseBackgroundImage.rectTransform.sizeDelta = new Vector2(Screen.currentResolution.width + 10, Screen.currentResolution.height + 10);
            pauseScreenFadeSpeed = 60;
        }
	}

    public void Update()
    {
        if (Input.GetButtonDown(pauseButtonName))
        {
            if (Time.timeScale > 0)
            {
                PauseGame();
                ActivateCurrentItem();
            }
            else
            {
                UnpauseGame();
                DeactivateCurrentItem();
            }
        }

        if (pauseBackgroundImage != null)
        {
            if (Time.timeScale <= 0)
            {
                pauseBackgroundColor.a += (1f / pauseScreenFadeSpeed);
            }
            else
            {
                pauseBackgroundColor.a -= (1f / pauseScreenFadeSpeed) * 1.5f;
            }
            pauseBackgroundColor.a = Mathf.Clamp(pauseBackgroundColor.a, 0f, 1f);
            pauseBackgroundImage.color = pauseBackgroundColor;
        }
    }

    public void LoadLastPlayedLevel()
    {
        Application.LoadLevel(lastPlayedLevel);
    }

    public void ActivateMenu(int menuItem)
    {
        //activeer het gekozen menu en deactiveer het huidige menu, sla daarna op wat het huidige menu is
        menuObjects[menuItem].SetActive(true);
        menuObjects[currentMenuItem].SetActive(false);
        currentMenuItem = menuItem;
    }

    public void LoadMain()
    {
        Application.LoadLevel("MainMenu");
    }

    public void QuitGame()
    {
        //Debug.Log("Quit button pressed!"); //Puur ter controle van de functionaliteit
        Application.Quit();
        //quit game logic hangt af van het platform dus dat komt nog
        //als het namelijk in een browser moet kunnen, mag dit spel niet de browser zelf afsluiten
    }

    public void LoadLevel(string levelToLoad)
    {
        Application.LoadLevel(levelToLoad);
    }

    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void NextLevel()
    {
        int nextLevelNumber = GetNextLevel();
        if (Application.CanStreamedLevelBeLoaded("Level" + nextLevelNumber))
            Application.LoadLevel("Level" + nextLevelNumber);
        else
            Application.LoadLevel("CreditScreen");
    }

    public void SaveProgress()
    {
        //lees het huidige level en sla het op als een int (we lezen het als string omdat de numering niet overeenkomt met het echte level)
        //int curLevel = int.Parse(Application.loadedLevelName.Substring(Application.loadedLevelName.Length - 1));
        //bereken wat het volgende level is en sla het op als string
        int nextLevelNumber = GetNextLevel() - 1;
        string nextLevel = "Level" + nextLevelNumber;

        //check of het level wel bestaat, zodat we geen niet-bestaand level opslaan
        if (Application.CanStreamedLevelBeLoaded(nextLevel) && nextLevelNumber > totalUnlockedLevels)
        {
            //als het volgende level bestaat slaan we dat op als last unlocked level (voor het level select menu)
            PlayerPrefs.SetInt("LastUnlockedLevel", nextLevelNumber);
        }
    }

    int GetNextLevel()
    {
        int nextLevel = 0;
        int i = 3;
        lastPlayedLevel = PlayerPrefs.GetString("LastPlayedLevel", "Level1");
        while (!int.TryParse(lastPlayedLevel.Substring(lastPlayedLevel.Length - i), out nextLevel))
        {
            i--;
        }
        nextLevel = int.Parse(lastPlayedLevel.Substring(lastPlayedLevel.Length - i)) + 1;

        return nextLevel;
    }

    void ResetLevelProgress()
    {
        PlayerPrefs.SetInt("LastUnlockedLevel", 1);
    }

    public void ActivateCurrentItem()
    {
        menuObjects[currentMenuItem].SetActive(true);
    }

    public void DeactivateCurrentItem()
    {
        menuObjects[currentMenuItem].SetActive(false);
    }
}