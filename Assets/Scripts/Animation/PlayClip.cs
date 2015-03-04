using UnityEngine;
using System.Collections;

public class PlayClip : MonoBehaviour {

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void PlayAnimation()
    {
        anim.Stop();
        anim.Play();
    }
}
