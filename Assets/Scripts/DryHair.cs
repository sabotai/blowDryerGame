using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryHair : MonoBehaviour
{
    public float speed = 15f;
    public AudioClip good, bad;
    public float originalDrag = 5f;
    public float lowDrag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col){
        if (col.transform.parent.GetComponent<LineRenderer>()){

            //col.transform.GetSiblingIndex();
        }
        if (col.gameObject.tag == "hair"){
            float radChange = speed;
            if (col.gameObject.GetComponent<Brush>() != null){
                Debug.Log("has brush script");
                if (col.gameObject.GetComponent<Brush>().brushActive) {
                    radChange *= -1f;
                    GetComponent<AudioSource>().PlayOneShot(good, 0.3f);

                }
                else {
                    radChange *= 0.1f;
                    GetComponent<AudioSource>().PlayOneShot(bad, 0.1f);
                }
            } else radChange = 0f;
            Debug.Log(radChange);
            col.gameObject.GetComponent<SphereCollider>().radius += radChange * Time.deltaTime;
            if (col.gameObject.GetComponent<SphereCollider>().radius < 1.1f) col.gameObject.GetComponent<SphereCollider>().radius = 1.1f;
        }
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "hair" && col.gameObject.GetComponent<Brush>()){
            if (col.gameObject.GetComponent<Brush>().brushActive){
                col.gameObject.GetComponent<Rigidbody>().drag = lowDrag + originalDrag / 2f;
            } else {
                col.gameObject.GetComponent<Rigidbody>().drag = lowDrag;
                col.gameObject.GetComponent<Rigidbody>().mass =1f;
            }
        }
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "hair" && col.gameObject.GetComponent<Rigidbody>()){
           col.gameObject.GetComponent<Rigidbody>().drag = originalDrag;
                col.gameObject.GetComponent<Rigidbody>().mass =20f;
       }
    }
}
