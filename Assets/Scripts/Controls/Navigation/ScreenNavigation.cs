using UnityEngine;
using System.Collections;

public class ScreenNavigation : MonoBehaviour {

    // Public Variables

    // Zoom
    public float minCamSize, maxCamSize, maxFingerDistance, maxWorldOffsetY;
    public Camera MainCamera;

    // Private Variables
    private Planet PlanetScript;

    // Zoom
    private Vector2 fingerPos1, fingerPos2;
    private float newCamSize, currentCamSize;
    private bool calcNewSizeFlag;

    // Turning
    private Vector2 startPos;
    private bool turningFlag, angleOffsetFlag;
    private float startAngle;

	// Use this for initialization
	void Start () {
	
        // General
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();

        // Zoom
        fingerPos1 = Vector2.zero;
        fingerPos2 = Vector2.zero;
        newCamSize = MainCamera.orthographicSize;
        calcNewSizeFlag = false;

        // Turning
        turningFlag = false;
        angleOffsetFlag = false;
        startAngle = transform.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {

        if (PlanetScript.GetCanControl())
        {
            // Zooming with two fingers
            ScreenZoom();

            // Moving the world
            ScreenTurning();
        }   
	}

    #region Zoom
    void ScreenZoom()
    {
        // First Finger
        if (Input.touchCount == 1)
        {
            // Get position of first finger
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fingerPos1 = Input.GetTouch(0).position;
            }    
        }

        // Second Finger
        if (Input.touchCount == 2)
        {
            // Get position of first finger
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fingerPos1 = Input.GetTouch(0).position;
            }   

            // Get position of second finger
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                fingerPos2 = Input.GetTouch(1).position;

                currentCamSize = MainCamera.orthographicSize;
                calcNewSizeFlag = true;
            }

            // Calculate the new cam size
            calcNewSize();
        }

        // Resetting flag
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    calcNewSizeFlag = false;
                }
            }
        }

        // Update with the new cam size
        UpdateCamSize();
    }

    void calcNewSize()
    {
        if (calcNewSizeFlag)
        {
            float tmpDistance = Vector2.Distance(fingerPos1, fingerPos2) - Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

            if (tmpDistance > maxFingerDistance)
            {
                tmpDistance = maxFingerDistance;
            }

            if (tmpDistance < -maxFingerDistance)
            {
                tmpDistance = -maxFingerDistance;
            }

            newCamSize = ((tmpDistance) / (maxFingerDistance) * (maxCamSize - minCamSize) + currentCamSize);

            if (newCamSize > maxCamSize)
            {
                fingerPos1 = Input.GetTouch(0).position;
                fingerPos2 = Input.GetTouch(1).position;

                currentCamSize = MainCamera.orthographicSize;

                newCamSize = maxCamSize;
            }

            if (newCamSize < minCamSize)
            {
                fingerPos1 = Input.GetTouch(0).position;
                fingerPos2 = Input.GetTouch(1).position;

                currentCamSize = MainCamera.orthographicSize;
                newCamSize = minCamSize;
            }    

        }        
    }

    void UpdateCamSize()
    {
        MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, newCamSize, 20 * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, Mathf.Lerp( transform.position.y, ((MainCamera.orthographicSize - minCamSize) / (maxCamSize - minCamSize)) * maxWorldOffsetY - maxWorldOffsetY, 5 * Time.deltaTime));
    }

    #endregion Zoom

    #region Turning

    void ScreenTurning()
    {
        if (Input.touchCount == 1)
        {
            Vector2 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startAngle = transform.eulerAngles.z;

                startPos = Input.GetTouch(0).position;

                test = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                RaycastHit2D hit = Physics2D.Raycast(test, startPos);

                if (hit.collider && (hit.collider.tag == "Planet" || hit.collider.tag == "LivingArea") )
                {
                    turningFlag = true;
                    angleOffsetFlag = true;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (turningFlag)
                {
                    if (angleOffsetFlag)
                    {
                        angleOffsetFlag = false;
                        startPos = Input.GetTouch(0).position;
                    }

                    Vector3 source = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
                    Vector3 target = Camera.main.ScreenToWorldPoint(startPos) - transform.position;

                    float angle = Mathf.DeltaAngle(Mathf.Atan2(source.y, source.x) * Mathf.Rad2Deg, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);

                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -angle + startAngle);
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                turningFlag = false;
            }
        }
    }

    #endregion Turning

}
