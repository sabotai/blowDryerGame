using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour {

    private LineRenderer lineRenderer;
    public float startW = 3f;
    public float endW = 1f;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();//gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = startW;
        lineRenderer.endWidth = endW;


    }

    void Update(){

        lineRenderer.startWidth = startW;
        lineRenderer.endWidth = endW;
        
        Transform[] children = GetComponentsInChildren<Transform>();
        lineRenderer.positionCount = children.Length;
        for (int i = 0; i < children.Length; i++) {
            lineRenderer.SetPosition(i, children[i].position);
        }
    }
}