using UnityEngine;
using System.Collections;

public class MusicKeepsPlaying : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);
    }
}
