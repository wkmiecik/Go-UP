using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    [SerializeField]
    GameObject leftBound, rightBound;

    float horzExtent;

    void Start()
    {
        // Set correct bound pos to screen size
        horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        leftBound.transform.position = new Vector2(-(horzExtent+2),0);
        rightBound.transform.position = new Vector2(horzExtent + 2, 0);
    }
}
