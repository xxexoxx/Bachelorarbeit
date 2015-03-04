using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    private float delay;
    private bool playing;

	// Use this for initialization
	void Start () {
        delay = Random.RandomRange(0, 15f);
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.timeSinceLevelLoad > delay && !playing)
        {
            playing = true;
            GetComponent<Animation>().Play();
        }
	}
}
