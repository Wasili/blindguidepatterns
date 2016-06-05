using UnityEngine;
using System.Collections;

public class PlayerPowerBehaviour : MonoBehaviour {

    public GameObject fireBall;
    public GameObject iceBall;
    GameObject[] pullableObjects;
    bool canShoot;
    float timer;
    float frameTimer;
    public float superPowerCooldown;
    SpriteRenderer spriteRenderer;
    //radiusBorder is het waar de pull begint, radiusStart is waar de pull eindigt zodra het object dichtbij is
    public float radiusStart, radiusBorder, pullSpeed, pushPower;
    int curSprite;

    bool humming, pulling;
    public AudioClip pullHum;

    GameObject tempPull = null;
    int currentPower = 0;
    Transform myTransform;
    public Transform superPowerSpawner;
    GameObject spawnedPower = null;

    public Sprite[] graviBarky;
    public Sprite[] iceBarky;
    public Sprite[] fireBarky;
    public float animationTime = 1;

    Sprite[] triggeredAnimation;

    string usePowerButtonName = "Use Power", switchPowerButtonName = "Switch Power";

    void Start () {
        //sla op wat het huidige level is, zodat dit wordt geladen als er op Continue wordt gedrukt bij het main menu
        PlayerPrefs.SetString("LastPlayedLevel", Application.loadedLevelName);
        pullableObjects = GameObject.FindGameObjectsWithTag("PullableObject");
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0;
        myTransform = transform;
        frameTimer = animationTime;

        DataCollector.LoadLocalMetricData();
        for (int i = 0; i < (int)DataCollector.Metric.METRIC_COUNT; i++)
        {
            DataCollector.AdjustDataMetric((DataCollector.Metric)i, 1);
        }

        for (int i = 0; i < (int)DataCollector.Metric.METRIC_COUNT; i++)
        {
            Debug.Log(((DataCollector.Metric)i).ToString() + ": " + DataCollector.GetMetricValue((DataCollector.Metric)i));
        }
        DataCollector.SaveAllMetricsLocally();
    }

    void Update()
    {
        if (Input.GetButtonDown(switchPowerButtonName))
        {
            timer = 0;
            currentPower += 1;
            if (spawnedPower != null)
                Destroy(spawnedPower);
            if (currentPower > 2)
            {
                currentPower = 0;
            }
        }

        GravitationalPull();
        
        switch (currentPower)
        {
            case 0:
                triggeredAnimation = graviBarky;
                AnimateBarky();
                break;
            case 1:
                SpawnSuperPower(fireBall);
                triggeredAnimation = fireBarky;
                AnimateBarky();
                break;
            case 2:
                SpawnSuperPower(iceBall);
                triggeredAnimation = iceBarky;
                AnimateBarky();
                break;
        }

        CooldownFireIce();
    }

    public void CooldownFireIce()
    {
        if (timer >= 0f)
        {
            canShoot = false;
            timer -= Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }
    }

    void SpawnSuperPower(GameObject powerObject)
    {
        if (Input.GetButtonDown(usePowerButtonName) && canShoot == true)
        {
            spawnedPower = (GameObject) Instantiate(powerObject, myTransform.position, myTransform.rotation);
            spawnedPower.GetComponent<SuperPowerFireIce>().SetSpawnSource(superPowerSpawner);
            spawnedPower.GetComponent<SuperPowerFireIce>().playerTransform = myTransform;
            timer = superPowerCooldown;
        }
    }

    void HandleFirePower()
    {
        if (Input.GetButtonDown(usePowerButtonName) && canShoot == true)
        {
            Instantiate(fireBall, superPowerSpawner.position, myTransform.rotation);
            timer = superPowerCooldown;        
        }
    }

    void HandleIcePower()
    {
        if (Input.GetButtonDown(usePowerButtonName) && canShoot == true)
        {
            Instantiate(iceBall, superPowerSpawner.position, myTransform.rotation);
            timer = superPowerCooldown;        
        }
    }

    void GravitationalPull()
    {
        pullableObjects = GameObject.FindGameObjectsWithTag("PullableObject");

        if (Input.GetButtonDown(usePowerButtonName) && !pulling)
        {
            pulling = true;
            return;
        }

        foreach (GameObject pullObj in pullableObjects)
        {
            if (pullObj.GetComponent<Rigidbody2D>() == null)
                continue;

            if (currentPower != 0)
            {
                pullObj.GetComponent<Rigidbody2D>().isKinematic = false;
                pulling = false;
                tempPull = null;
                continue;
            }

            float distance = Vector2.Distance(transform.position, pullObj.transform.position);
            if (distance < radiusBorder && distance > radiusStart)
            {
                if (pulling)
                {
                    if (tempPull == null)
                        tempPull = pullObj;

                    if (tempPull == pullObj)
                    {
                        if (!humming)
                        {
                            playSound(pullHum);
                            humming = true;
                        }
                        Vector3 objVelocity = (transform.position - pullObj.transform.position) * Time.deltaTime * pullSpeed;
                        pullObj.transform.position += objVelocity;
                        pullObj.GetComponent<Rigidbody2D>().isKinematic = true;

                        if (Input.GetButtonDown(usePowerButtonName))
                        {
                            pullObj.GetComponent<Rigidbody2D>().isKinematic = false;
                            pullObj.GetComponent<Rigidbody2D>().velocity = GlobalGuide.AimTowardsMouse(pullObj.transform.position) * pushPower;
                            tempPull = null;
                            pulling = false;
                        }
                    }
                }
            }
            else
            {
                if (pullObj == tempPull)
                {
                    pulling = false;
                    tempPull = null;
                }
                pullObj.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }

        if (tempPull == null)
            pulling = false;

        if (!pulling)
        {
            humming = false;
            GetComponent<AudioSource>().Stop();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(transform.position, radiusBorder);
    }

    public void playSound(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }

    void AnimateBarky()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            curSprite++;
            if (curSprite >= triggeredAnimation.Length)
            {
                curSprite = 0;
            }
            frameTimer = animationTime;
        }
        spriteRenderer.sprite = triggeredAnimation[curSprite];
    }
}