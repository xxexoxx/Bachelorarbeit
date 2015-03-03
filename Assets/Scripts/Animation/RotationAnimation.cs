using UnityEngine;
using System.Collections;

public class RotationAnimation : MonoBehaviour, IAnimation {

    // Public Variables
    public float rotationSpeed;

    // private Variables
    private bool animationActive;

	// Use this for initialization
    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        if (TypeOfAudiovisualEffects == AudiovisualEffects.On)
        {
            animationActive = true;
        }
        else
        {
            animationActive = false;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (animationActive)
        {
            transform.RotateAround(transform.position, -Vector3.forward, rotationSpeed * Time.deltaTime);
        } 
	}
}
