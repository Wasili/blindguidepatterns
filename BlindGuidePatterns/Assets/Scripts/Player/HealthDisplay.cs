using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public Text display;

    void Start()
    {
        display = GetComponent<Text>();
    }

    void Update()
    {
        display.text = "Health remaining: " + BlindDamage.health;
    }
}