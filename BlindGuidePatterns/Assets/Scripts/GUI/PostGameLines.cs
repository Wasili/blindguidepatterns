using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PostGameLines : MonoBehaviour {

    public float timer, fadeIn;
    int curText = 0;
    public float timeSwitch;
    public Text[] texts;
    bool done = false;

    void Start()
    {
        Line1();
        timer = timeSwitch;
    }

    void Update()
    {
        if (done)
        {
            Application.LoadLevel("CreditScreen");
        }
        timer -= Time.deltaTime;
        if (timer <= 2)
        {
            texts[curText].color = new Color(texts[curText].color.r,
                texts[curText].color.g,
                texts[curText].color.b,
                timer * 0.5f);
            if (timer <= 0)
            {
                timer = timeSwitch;
            }
        }
        else
        {
            if (fadeIn < 1)
            {
                fadeIn += Time.deltaTime * 0.5f;
                texts[curText].color = new Color(texts[curText].color.r,
                        texts[curText].color.g,
                        texts[curText].color.b,
                        fadeIn);
            }
        }
    }

    void Line1()
    {
        texts[0].gameObject.SetActive(true);
        Invoke("Line2", timeSwitch);
    }
    void Line2()
    {
        SwitchMe();
        texts[0].gameObject.SetActive(false);
        texts[1].gameObject.SetActive(true);
        Invoke("Line3", timeSwitch);
    }
    void Line3()
    {
        SwitchMe();
        texts[1].gameObject.SetActive(false);
        texts[2].gameObject.SetActive(true);
        Invoke("Line4", timeSwitch);
    }

    void Line4()
    {
        SwitchMe();
        texts[2].gameObject.SetActive(false);
        texts[3].gameObject.SetActive(true);
        Invoke("LoadCredits", timeSwitch);
    }
    void LoadCredits()
    {
        done = true;
    }

    void SwitchMe()
    {
        curText += 1;
        fadeIn = 0;
        timer = timeSwitch;
    }
}
