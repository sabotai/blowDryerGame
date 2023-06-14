using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour {

    private LineRenderer lineRenderer;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();//gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 3f;
        lineRenderer.endWidth = 1f;


    }

    void Update(){
        Transform[] children = GetComponentsInChildren<Transform>();
        lineRenderer.positionCount = children.Length;
        for (int i = 0; i < children.Length; i++) {
            lineRenderer.SetPosition(i, children[i].position);
        }
    }
}