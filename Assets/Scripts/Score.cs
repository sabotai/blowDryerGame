using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float goodPoints;
    public static float badPoints;
    public static float score;
    public float good = 0f;
    public float bad = 0f;
    public float publicScore;
    // Start is called before the first frame update
    void Start()
    {
        goodPoints = 0f;
        badPoints = 0f;
        publicScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        good = goodPoints;
        bad = badPoints;
        publicScore = score;
    }
}
