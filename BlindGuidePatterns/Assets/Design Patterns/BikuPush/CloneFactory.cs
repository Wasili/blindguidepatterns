using UnityEngine;
using System.Collections;

public class CloneFactory : MonoBehaviour {
    public Prototype snowman, monkey;
    //We create a snowman and we clone it if it already exists and same with monkey
    void CreateSnowman() {
        if (snowman == null) {
            snowman = Instantiate(snowman.gameObject).GetComponent<Prototype>();
        }
        else
        {
            Instantiate(snowman.Clone().gameObject);
        }
    }

    void CreateMonkey()
    {
        if (monkey == null)
        {
            monkey = Instantiate(monkey.gameObject).GetComponent<Prototype>();
        }
        else
        {
            Instantiate(monkey.Clone().gameObject);
        }
    }
}
