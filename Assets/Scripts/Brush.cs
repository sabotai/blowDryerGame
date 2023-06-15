using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public bool brushActive = false;
    public float originalDrag;
    // Start is called before the first frame update
    void Start()
    {
        originalDrag = GetComponent<Rigidbody>().drag;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "brush") brushActive = true;
        
    }
    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "brush") brushActive = false;
    }
}
