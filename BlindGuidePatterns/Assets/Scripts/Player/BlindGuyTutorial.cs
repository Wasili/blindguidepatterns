using UnityEngine;
using System.Collections;

public class BlindGuyTutorial : MonoBehaviour {
    public enum states { start, normal, pause };
    public states myState = states.start;

    GameObject eventObject;
    Rigidbody2D eventBody;
    Geyser eventGeyser;

    bool pullEvent = false;

    public float pullableThrowCheckTime;
    float pullableThrowCheckTimer;

    BlindGuyAI blindGuyAI;

    public SpriteRenderer instructionScreen;
    public Sprite[] instructions;

    GameObject player;
    PlayerMovement playerMovement;
    GrenadeBehaviour playerBark;

	void Start() 
    {
        blindGuyAI = gameObject.GetComponent<BlindGuyAI>();
        blindGuyAI.enabled = false;
        pullableThrowCheckTimer = 0;
        instructionScreen.sprite = instructions[0];
	}

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerBark = player.GetComponent<GrenadeBehaviour>();
        playerBark.enabled = false;
    }

    void GranadeInstruction()
    {
        NewInstruction(1);
    }

    void NewInstruction(int value)
    {
        if (value < 0)
        {
            instructionScreen.sprite = null;
            return;
        }
        instructionScreen.sprite = instructions[value];
    }
	
	void Update() 
    {
	    switch (myState) 
        {
            case states.start:
                WaitForPlayer();
                break;
            case states.normal:
                Walk();
                break;
            case states.pause:
                Idle();
                break;
        }
        instructionScreen.transform.localScale = transform.localScale;
	}

    void WaitForPlayer()
    {
        //wait for player movement
        if (instructionScreen.sprite == instructions[0])
        {
            if (playerMovement.velocity.x != 0 || playerMovement.velocity.y != 0)
            {
                playerBark.enabled = true;
                NewInstruction(1);
            }
        }
            //wait for bark
        else if (instructionScreen.sprite == instructions[1])
        {
            if (playerBark.grenadeCooldown > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                blindGuyAI.enabled = true;
                myState = states.normal;
            }
        }
    }

    void Walk()
    {
        if (!blindGuyAI.enabled) 
        {
            OnWalkActivate();
        }
    }

    void Idle()
    {
        if (blindGuyAI.enabled)
        {
            OnIdleActivate();
        }

        if (pullEvent)
            PullEvent();
        else
            if (eventGeyser == null)
                ShootEvent();
            else
                FreezeEvent();
    }

    void FreezeEvent()
    {
        NewInstruction(5);
        if (eventGeyser.frozenState)
        {
            NewInstruction(-1);
            myState = states.normal;
            return;
        }
    }

    void ShootEvent()
    {
        NewInstruction(4);
        if (eventObject == null)
        {
            NewInstruction(3);
            myState = states.normal;
            return;
        }
    }

    void PullEvent()
    {
        if (eventBody == null)
        {
            NewInstruction(3);
            myState = states.normal;
            return;
        }

        if (pullableThrowCheckTimer < 0)
        {
            if (eventBody.transform.position.x < transform.position.x)
                eventBody = null;
            else
            {
                NewInstruction(2);
                eventBody.transform.position = eventBody.GetComponent<EventItem>().startPos;
                pullableThrowCheckTimer = 0;
            }
            return;
        }

        if (eventBody.isKinematic)
        {
            pullableThrowCheckTimer = pullableThrowCheckTime;
            return;
        }

        if (pullableThrowCheckTimer > 0)
        {
            pullableThrowCheckTimer -= Time.deltaTime;
            return;
        }
    }

    void OnWalkActivate()
    {
        blindGuyAI.enabled = true;
    }

    void OnIdleActivate()
    {
        if (pullEvent)
            NewInstruction(2);
        blindGuyAI.enabled = false;
    }

    public void StartEvent(GameObject eventObject)
    {
        pullEvent = false;
        this.eventObject = eventObject;
        myState = states.pause;
    }

    public void StartEvent(Rigidbody2D eventBody)
    {
        pullEvent = true;
        this.eventBody = eventBody;
        myState = states.pause;
    }

    public void StartEvent(Geyser eventGeyser)
    {
        pullEvent = false;
        this.eventGeyser = eventGeyser;
        myState = states.pause;
    }
}
