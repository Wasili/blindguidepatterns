using UnityEngine;
using System.Collections;

public class BarkCooldownVisual : MonoBehaviour {
    public Transform barkEffectObject, cooldownEffectObject, playerTransform;
    public float effectActiveDuration = 1f, effectActiveSmooth = 0.5f, effectInactiveDuration = 2f, cooldownEffectRotationSpeed = 360, barkEffectDuration = 1f, barkEffectSpeed = 10f;
    public Vector3 effectTargetSize = new Vector3(2, 2, 1);
    float inactiveTimer, activeTimer, disabledTimer, barkPlayerCooldown, barkEffectTimer, activeEffectSpeed = 1f;
    Vector3 targetSize, startScale;
    SpriteRenderer effectRenderer;
    Color startColor;
    public TextMesh cooldownTextObject;
    public SpriteRenderer cooldownTextBackground;
    public Vector3 cooldownTextOffset;
    public float cooldownTextFadeSpeed = 1f;
    Color cooldownTextColor;
    float cooldownTextFade = 0f;

    enum State { active, inactive, disabled };
    State curState;

	void Start () 
    {
        SetActive();
        if (barkEffectObject == null)
        {
            barkEffectObject = GameObject.Find("clock_ready").transform;
        }
        inactiveTimer = effectInactiveDuration;
        activeTimer = effectActiveDuration;
        effectTargetSize -= barkEffectObject.localScale;
        targetSize = effectTargetSize;
        effectRenderer = barkEffectObject.GetComponent<SpriteRenderer>();
        startColor = effectRenderer.color;
        startScale = barkEffectObject.localScale;

        cooldownTextObject.transform.parent = null;
        cooldownTextObject.transform.rotation = new Quaternion();
        cooldownTextObject.transform.position = playerTransform.position + cooldownTextOffset;
        cooldownTextObject.text = "";
        cooldownTextColor = cooldownTextObject.color;
	}

    void AnimateBarkVisual()
    {
        activeTimer -= Time.deltaTime;
        Vector3 sizeIncrease = targetSize * ((effectActiveDuration) * Time.deltaTime) * activeEffectSpeed;
        activeEffectSpeed -= effectActiveSmooth * Time.deltaTime;
        activeEffectSpeed = Mathf.Clamp(activeEffectSpeed, 0.1f, 1f);
        barkEffectObject.localScale += sizeIncrease;
        effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, (activeTimer / effectActiveDuration));

        if (activeTimer <= 0)
        {
            SetInactive();
        }
    }

    void WaitForAnimation()
    {
        inactiveTimer -= Time.deltaTime;
        if (inactiveTimer <= 0)
        {
            SetActive();
        }
    }

    void WaitForCooldown()
    {
        cooldownTextObject.text = disabledTimer.ToString("F1");
        if (Input.GetButton("Bark"))
        {
            cooldownTextFade = 1;
        }
        else
        {
            cooldownTextFade -= Time.deltaTime * cooldownTextFadeSpeed;
        }
        cooldownTextObject.color = new Color(cooldownTextColor.r, cooldownTextColor.g, cooldownTextColor.b, cooldownTextFade);
        cooldownTextBackground.color = new Color(cooldownTextBackground.color.r, cooldownTextBackground.color.g, cooldownTextBackground.color.b, cooldownTextObject.color.a);

        barkEffectTimer -= Time.deltaTime;
        if (barkEffectTimer > 0)
        {
            barkEffectObject.localScale += new Vector3(Time.deltaTime * barkEffectSpeed, Time.deltaTime * barkEffectSpeed, 0);
            effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, 0.2f);
        }
        else
        {
            barkEffectObject.localScale = startScale;
            effectRenderer.color = startColor;
        }

        disabledTimer -= Time.deltaTime;

        if (playerTransform.localScale.y < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        float angle = cooldownEffectObject.eulerAngles.z + ((cooldownEffectRotationSpeed * (disabledTimer / barkPlayerCooldown)) * Time.deltaTime);
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        cooldownEffectObject.rotation = newRotation;

        if (disabledTimer <= 0)
        {
            SetActive();
        }
    }

    void Update()
    {
        switch (curState) 
        {
            case State.active:
                AnimateBarkVisual();
                break;
            case State.inactive:
                WaitForAnimation();
                break;
            case State.disabled:
                WaitForCooldown();
                break;
        }
        cooldownTextObject.transform.position = playerTransform.position + cooldownTextOffset;
    }

    public void SetInactive()
    {
        if (curState != State.inactive)
        {
            barkEffectObject.localScale = startScale;
            effectRenderer.color = startColor;
            inactiveTimer = effectInactiveDuration;
        }

        curState = State.inactive;
    }

    public void SetActive()
    {
        if (curState != State.active)
        {
            cooldownTextObject.color = new Color(cooldownTextColor.r, cooldownTextColor.g, cooldownTextColor.b, 0);
            activeTimer = effectActiveDuration;
            barkEffectObject.localScale = startScale;
            activeEffectSpeed = 1f;
        }
        curState = State.active;
    }

    public void SetDisabled(float barkCooldown)
    {
        if (curState != State.disabled)
        {
            barkEffectTimer = barkEffectDuration;
            barkEffectObject.localScale = startScale;
            effectRenderer.color = startColor;

            barkPlayerCooldown = barkCooldown;
            disabledTimer = barkCooldown;
        }

        curState = State.disabled;
    }
}
