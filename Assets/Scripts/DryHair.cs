using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryHair : MonoBehaviour
{
    public float speed = 15f;
    public AudioClip good, bad;
    public float originalDrag = 5f;
    public float lowDrag = 0.1f;
    public float maxRad = 5f;
    public float minRad = 1f;
    public ParticleSystem part;

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
            bool goodChange = true;
            float radChange = speed;
            if (col.gameObject.GetComponent<Brush>() != null){
                //Debug.Log("has brush script");
                if (col.gameObject.GetComponent<Brush>().brushActive) {
                    radChange *= -1f;
                    goodChange = true;
                }
                else {
                    radChange *= 1f;
                    goodChange = false;
                }
            } else radChange = 0f;

            SphereCollider sColl = col.gameObject.GetComponent<SphereCollider>();
            float radPct = (sColl.radius - minRad) / (maxRad - minRad);
            Debug.Log("radChange = " + radChange + ", radPct = " + radPct);
            //change the points excluding strands that are almost done
            if (!goodChange && radPct < 1f){

                    GetComponent<AudioSource>().PlayOneShot(bad, 0.1f);
                    Score.badPoints++;
                    Score.score--;
            }
            if (goodChange && radPct > 0f) {

                    GetComponent<AudioSource>().PlayOneShot(good, 0.3f);
                    Score.goodPoints++;
                    Score.score++;
            }

            sColl.radius += radChange * Time.deltaTime;
            
            //cap it
            if (sColl.radius < minRad) sColl.radius = minRad;
            if (sColl.radius > maxRad) sColl.radius = maxRad;
            radPct = (sColl.radius - minRad) / (maxRad - minRad);
            
            if (col.transform.parent.GetComponent<StrandZoneStatus>()){
                //Debug.Log("updating color");
                StrandZoneStatus strand = col.transform.parent.GetComponent<StrandZoneStatus>();
                Color conversionColor;
                if (radPct < 0.5f){
                    conversionColor = strand.goodDryColor; 
                    radPct = 1f - (radPct * 2f);
                } else {
                    conversionColor = strand.badDryColor;
                    radPct = (radPct * 2f) -1f;
                }
                Color oldColor = strand.colorKey[col.transform.GetSiblingIndex()].color;
                //Debug.Log("oldColor = " + oldColor);
                strand.colorKey[col.transform.GetSiblingIndex()].color = Color.Lerp(oldColor, conversionColor, radPct);
                //Debug.Log("newColor = " + strand.colorKey[col.transform.GetSiblingIndex()].color);
                strand.UpdateColor();
            }
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

            var trails = part.trails;
            if (col.gameObject.tag == "brush") trails.colorOverLifetime = Manager.goodColor;
            
        
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "hair" && col.gameObject.GetComponent<Rigidbody>()){
           col.gameObject.GetComponent<Rigidbody>().drag = originalDrag;
                col.gameObject.GetComponent<Rigidbody>().mass =20f;
       }
            var trails = part.trails;
       if (col.gameObject.tag == "brush") trails.colorOverLifetime = Manager.badColor;
    }
}
