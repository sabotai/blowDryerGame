using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandZoneStatus : MonoBehaviour
{
    public Gradient grad;
    public Color initColorStart, initColorEnd, goodDryColor, badDryColor;
    public GradientColorKey[] colorKey; //= new GradientColorKey[transform.childCount];
    public GradientAlphaKey[] alphaKey; //= new GradientAlphaKey[transform.childCount];
    public 
    // Start is called before the first frame update
    void Start()
    {
        goodDryColor = Manager.goodColor;
        badDryColor = Manager.badColor;
        int childCount = 8;
        if (transform.childCount < 9){
            childCount = transform.childCount;
        }
        Debug.Log("childcount = " + transform.childCount);
        colorKey = new GradientColorKey[childCount];
        alphaKey = new GradientAlphaKey[childCount];

        for (int i = 0; i < colorKey.Length; i++){
            Debug.Log("i = " + i + ", percentage = " + (float)i / (float)colorKey.Length);
            colorKey[i].color = Color.Lerp(initColorStart, initColorEnd, (float)i / (float)colorKey.Length);
            colorKey[i].time = (float)i / (float)colorKey.Length;
            alphaKey[i].alpha = 1.0f;
            alphaKey[i].time = (float)i / (float)colorKey.Length;
        }
        /*
        colorKey[1].color = Color.blue;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 1.0f;

        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1f;
        */

        //for (int i = 0; i < transform.childCount; i++){

            //startingGrad.SetKeys(colorKey[1], alphaKey[0]);
            //startingGrad.SetKeys(colorKey[2], alphaKey[0]);
        //}
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateColor(){
        grad.SetKeys(colorKey, alphaKey);
        GetComponent<LineRenderer>().colorGradient = grad;
    }
}
