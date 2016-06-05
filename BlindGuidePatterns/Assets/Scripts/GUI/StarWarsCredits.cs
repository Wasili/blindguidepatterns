using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarWarsCredits : MonoBehaviour {
    public float scrollSpeed = 1f, timeToShowLogo = 25, timeToLoadMainMenu = 30;
    public GameObject[] textObjects;
    public Image logoImage;
    float logoFadeValue = 0;
    public float logoSpeed = 2f;
	
	void Update () 
    {
        timeToLoadMainMenu -= Time.deltaTime;
        timeToShowLogo -= Time.deltaTime;

        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null)
            {
                textObjects[i].transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
            }
        }

        if (timeToShowLogo <= 0 && logoImage != null)
        {
            logoFadeValue += Time.deltaTime * logoSpeed;
            logoImage.color = new Color(1, 1, 1, logoFadeValue);
            logoImage.transform.position += ((Camera.main.transform.position + new Vector3(0, 0, 0.93f)) - logoImage.transform.position) * Time.deltaTime * logoSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || timeToLoadMainMenu <= 0) 
        {
            LoadMainMenu();
        }
	}

    void LoadMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
