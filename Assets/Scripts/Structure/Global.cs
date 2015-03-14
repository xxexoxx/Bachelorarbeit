using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Global : MonoBehaviour {

    // Public Variables
    public GameObject DetailsTree;
    public EventSystem Events;

    public GameObject GetDetailsTree()
    {
        return DetailsTree;
    }

    public EventSystem GetEventSystem()
    {
        return Events;
    }
}
