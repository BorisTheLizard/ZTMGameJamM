using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    public GameObject[] points;

    void Awake()
    {
        // Get the number of children objects under the parent object
        int childCount = transform.childCount;

        // Initialize the children array with the appropriate size
        points = new GameObject[childCount];

        // Loop through each child object and add it to the children array
        for (int i = 0; i < childCount; i++)
        {
            points[i] = transform.GetChild(i).gameObject;
            points[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
