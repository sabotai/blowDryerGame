using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    //public InputAction playerControl;
    public PlayerInput playerControls;
    private InputAction move, look, lAlt, rAlt, lTrig, rTrig;
    public Transform leftView, rightView, neutral;
    public Transform mech, blowDryer, brush, blowDryer2, brush2, blowDryerCol;
    public bool dryerOn = true;

    Vector2 moveDirection, lookDirection;

    [SerializeField] private Basic basic; 

    bool lA = false;
    bool rA = false;
    bool lT = false;
    bool rT = false;

    void Awake(){

        basic = new Basic();
    }

    void Start(){
        basic.Player.LeftAlt.started += LAltStarted;
        basic.Player.LeftAlt.performed += LAltPerformed;
        basic.Player.LeftAlt.canceled += LAltCanceled;
        basic.Player.RightAlt.started += RAltStarted;
        basic.Player.RightAlt.canceled += RAltCanceled;
        basic.Player.LeftTrigger.started += LTrigStarted;
        basic.Player.LeftTrigger.canceled += LTrigCanceled;
        basic.Player.RightTrigger.started += RTrigStarted;
        basic.Player.RightTrigger.canceled += RTrigCanceled;

    }

    private void OnEnable(){
        move = playerControls.actions["Move"];
        move.Enable();
        look = playerControls.actions["Look"];
        look.Enable();

        lAlt = playerControls.actions["LeftAlt"];
        lAlt.Enable();
        rAlt = playerControls.actions["RightAlt"];
        rAlt.Enable();

        lTrig = playerControls.actions["LeftTrigger"];
        lTrig.Enable();
        rTrig = playerControls.actions["RightTrigger"];
        rTrig.Enable();


        basic.Enable();
    }
    private void OnDisable(){
        move.Disable();
        basic.Disable();
        look.Disable();
        lAlt.Disable();
        rAlt.Disable();
        lTrig.Disable();
        rTrig.Disable();
    }
    

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        lookDirection = look.ReadValue<Vector2>();
        //if (moveDirection.x < 0f) transform.position = Vector3.Slerp(leftView.position, neutral.position, 1f + moveDirection.x);
        //else if (moveDirection.x > 0f)  transform.position = Vector3.Slerp(rightView.position, neutral.position, 1f - moveDirection.x);
        //mech.rotation = Quaternion.Slerp(Quaternion.Euler(0f, -90f, 0f), Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
        //Debug.Log(lAlt.ReadValueAsObject());
        if (lA){//lAlt.ReadValueAsObject()){

                mech.rotation = Quaternion.Euler(moveDirection.y * 30f, moveDirection.x * -30f, 0f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                Vector3 camRot = new Vector3(mech.rotation.eulerAngles.x * -1f, mech.rotation.eulerAngles.y * -1f, mech.rotation.eulerAngles.z * -1f);
                transform.parent.rotation = Quaternion.Euler(camRot);
                //transform.LookAt(mech);
                //transform.eulerAngles = new Vector3 (Mathf.Clamp(transform.eulerAngles.x, -100f, 100f), Mathf.Clamp(transform.eulerAngles.y, -100f, 100f), 0f);

            } else if (!lT) {   
                //brush.rotation = Quaternion.Euler(moveDirection.y * 180f, 0f, moveDirection.x * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                //brush2.rotation = Quaternion.Euler(moveDirection.y * -180f, 0f, moveDirection.x * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                brush.rotation = Quaternion.Euler(0f, moveDirection.x * 180f, moveDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                brush2.rotation = Quaternion.Euler(0f, moveDirection.x * -180f, moveDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
          }
       if (rA) {
            //transform.LookAt(mech);
            //Vector3 eyeRot = 
            transform.rotation = Quaternion.Euler(lookDirection.y * -15f, lookDirection.x * 20f, 0);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
            transform.rotation *= Quaternion.Euler(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z);
        } else {

            blowDryer.rotation = Quaternion.Euler(0, lookDirection.x * 180f + 180f, lookDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
            blowDryer2.rotation = Quaternion.Euler(0, lookDirection.x * -180f - 180f, lookDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);

        }

        blowDryerCol.gameObject.SetActive(dryerOn);
    }

    void LAltPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("left alt performed");
    }
    void LAltStarted(InputAction.CallbackContext context)
    {
        Debug.Log("left alt started");
        lA = true;
    }
    void LAltCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("left alt canceled");
        lA = false;
    }
    void RAltCanceled(InputAction.CallbackContext context)
    {
        rA = false;
        transform.rotation = transform.parent.rotation;//Quaternion.Euler(0f, 0f, 0f);
    }
    void RAltStarted(InputAction.CallbackContext context)
    {
        Debug.Log("right alt started");
        rA = true;
    }

    void LTrigCanceled(InputAction.CallbackContext context)
    {
        lT = false;
    }
    void LTrigStarted(InputAction.CallbackContext context)
    {
        lT = true;
    }
    void RTrigCanceled(InputAction.CallbackContext context)
    {
        
    }
    void RTrigStarted(InputAction.CallbackContext context)
    {
        dryerOn = !dryerOn;
    }

}
