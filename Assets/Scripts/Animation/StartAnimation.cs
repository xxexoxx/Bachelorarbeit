using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

    // private Variables
    private GlobalAdmin GlobalAdminScript;

    void Start()
    {
        GlobalAdminScript = GameObject.Find("GlobalAdmin").GetComponent<GlobalAdmin>();
        GlobalAdminScript.AddAnimationObject(gameObject);
    }

    public void StartAllAnimations(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        SendMessage("Animate", TypeOfAudiovisualEffects, SendMessageOptions.DontRequireReceiver);
    }
}
