using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarkCooldown : MonoBehaviour
{
    public Sprite cooldown;
    public SpriteRenderer spriteRenderer;
    float fade = 1, barkCooldown, barkPercent, curCooldown;
    GrenadeBehaviour grenadeScript;

    void Start()
    {
        grenadeScript = GetComponent<GrenadeBehaviour>();
        barkCooldown = grenadeScript.grenadeMaxCD;
    }

    void Fading()
    {
        curCooldown = grenadeScript.grenadeCooldown;
        fade = curCooldown / barkCooldown;
    }

    void Update()
    {
        Fading();
        spriteRenderer.color = new Color(255,255,255, fade);
    }
}