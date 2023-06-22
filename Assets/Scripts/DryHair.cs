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
    public float rootColorOffset = .02f;

    public float[] startTime = new float[5];
    public float burnTime, smokeTime, fireTime;
    public Transform[] mostRecentStrands = new Transform[5];
    public GameObject smoke, fire;


    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < startTime.Length; i++) startTime[i] = 0f;
            
    }

    void OnDisable(){

        for (int i = 0; i < mostRecentStrands.Length; i++) mostRecentStrands[i] = null; //clear them when you turn it off
    }
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < mostRecentStrands.Length; i++){
            if (Time.time > startTime[i] + smokeTime && mostRecentStrands[i] != null){

             Instantiate(smoke, mostRecentStrands[i]);//.position, Quaternion.identity);

         }
             if (Time.time > startTime[i] + fireTime && mostRecentStrands[i] != null){


             Instantiate(fire, mostRecentStrands[i].position, Quaternion.identity);


                StrandZoneStatus strand = mostRecentStrands[i].parent.GetComponent<StrandZoneStatus>();
                Color oldColor = strand.colorKey[mostRecentStrands[i].GetSiblingIndex()].color;
                //Debug.Log("oldColor = " + oldColor);
                strand.colorKey[mostRecentStrands[i].GetSiblingIndex()].color = Color.black;//.Lerp(oldColor, Color.black, 500f * Time.deltaTime);
                strand.UpdateColor();
            }
            if (Time.time > startTime[i] + burnTime && mostRecentStrands[i] != null)
             Destroy(mostRecentStrands[i].parent.gameObject);
         

        } 
    }

    void OnCollisionEnter(Collision col){
        if (col.transform.parent.GetComponent<LineRenderer>()){

            //col.transform.GetSiblingIndex();
        }

        if (col.gameObject.tag == "hair"){
            if (mostRecentStrands[0] != col.transform){
                for (int i = 0; i < mostRecentStrands.Length; i++){
                    if (i < mostRecentStrands.Length - 1){
                        mostRecentStrands[i+1] = mostRecentStrands[i];
                        startTime[i+1] = startTime[i];
                    }
                    startTime[0] = Time.time;
                    mostRecentStrands[0] = col.transform;
                }

            }
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
                conversionColor = new Color(conversionColor.r + rootColorOffset * col.transform.GetSiblingIndex(), conversionColor.g + rootColorOffset * col.transform.GetSiblingIndex(), conversionColor.b + rootColorOffset * col.transform.GetSiblingIndex(), 1f);
                Color oldColor = strand.colorKey[col.transform.GetSiblingIndex()].color;
                //Debug.Log("oldColor = " + oldColor);
                if (oldColor != Color.black)  {
                    strand.colorKey[col.transform.GetSiblingIndex()].color = Color.Lerp(oldColor, conversionColor, radPct);
                    //Debug.Log("newColor = " + strand.colorKey[col.transform.GetSiblingIndex()].color);
                    strand.UpdateColor();
                }
            }
        }
    }

    void OnCollisionExit(Collision col){
        /*
        if (mostRecentStrand == col.transform){
            startTime = Time.time;
            mostRecentStrand = null;
        }
        */
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
