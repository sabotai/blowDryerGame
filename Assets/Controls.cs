using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    //public InputAction playerControl;
    public PlayerInput playerControls;
    private InputAction move, look, lAlt;
    public Transform leftView, rightView, neutral;
    public Transform mech, blowDryer, brush, blowDryer2, brush2;

    Vector2 moveDirection, lookDirection;

    [SerializeField] private Basic basic; 
    bool lA = false;

    void Awake(){

        basic = new Basic();
    }

    void Start(){
        basic.Player.LeftAlt.started += LAltStarted;
        basic.Player.LeftAlt.performed += LAltPerformed;
        basic.Player.LeftAlt.canceled += LAltCanceled;

    }

    private void OnEnable(){
        move = playerControls.actions["Move"];
        move.Enable();
        look = playerControls.actions["Look"];
        look.Enable();

        lAlt = playerControls.actions["LeftAlt"];
        lAlt.Enable();

        basic.Enable();
    }
    private void OnDisable(){
        move.Disable();
        basic.Disable();
        look.Disable();
        lAlt.Disable();
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

                mech.rotation = Quaternion.Euler(moveDirection.y * 30f, moveDirection.x * 30f, 0f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                Vector3 camRot = new Vector3(mech.rotation.eulerAngles.x * -1f, mech.rotation.eulerAngles.y * -1f, mech.rotation.eulerAngles.z * -1f);
                transform.parent.rotation = Quaternion.Euler(camRot);
                //transform.LookAt(mech);
                //transform.eulerAngles = new Vector3 (Mathf.Clamp(transform.eulerAngles.x, -100f, 100f), Mathf.Clamp(transform.eulerAngles.y, -100f, 100f), 0f);

            } else {   
                //brush.rotation = Quaternion.Euler(moveDirection.y * 180f, 0f, moveDirection.x * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                //brush2.rotation = Quaternion.Euler(moveDirection.y * -180f, 0f, moveDirection.x * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                brush.rotation = Quaternion.Euler(0f, moveDirection.x * 180f, moveDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
                brush2.rotation = Quaternion.Euler(0f, moveDirection.x * -180f, moveDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
          }
       blowDryer.rotation = Quaternion.Euler(0, lookDirection.x * 180f + 180f, lookDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);
       blowDryer2.rotation = Quaternion.Euler(0, lookDirection.x * -180f - 180f, lookDirection.y * 180f);//, Quaternion.Euler(0f, 90f, 0f), (moveDirection.x + 1f) /2f);

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
}