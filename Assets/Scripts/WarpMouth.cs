using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMouth : MonoBehaviour
{

    public Transform brush, dryer;
    float howFar, maxDist, origY;
    public float minDist;

    // Start is called before the first frame update
    void Start()
    {
        maxDist = Vector3.Distance(brush.position, dryer.position);
        origY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        howFar = Vector3.Distance(brush.position, dryer.position);
        float pct = howFar / (maxDist - minDist);

        transform.localScale = new Vector3(transform.localScale.x, origY * (pct * 2f - 1f), transform.localScale.z);

    }
}
