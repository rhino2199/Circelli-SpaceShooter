/****
 * Created by: Ryan Circelli
 * Date Created: Sept 13, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 15,2021
 * 
 * Description:Player control Movements
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*****VARIABLES*****/
    public bool MouseLook = true; // are we looking at mouse
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    public float MaxSpeed = 5f;// speed
    public float ReloadDelay = 0.3f;
    public bool CanFire = true;
    public Transform[] TurretTransforms;

    private Rigidbody ThisBody = null; //variable for the ship's rigidbody


    private void Awake()
    {
        ThisBody = GetComponent<Rigidbody>();//sets the object's rigidbody to variable
    }//End awake

    private void FixedUpdate()
    {
        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);

        Vector3 MoveDirection = new Vector3(Horz, 0.0f, Vert);
        ThisBody.AddForce(MoveDirection.normalized * MaxSpeed);

        ThisBody.velocity = new Vector3(Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(ThisBody.velocity.y, -MaxSpeed, MaxSpeed), Mathf.Clamp(ThisBody.velocity.z, -MaxSpeed, MaxSpeed));


        if ((Input.GetButtonDown(FireAxis) && CanFire))
        {
            foreach(Transform T in TurretTransforms)
            {
                AmmoManager.SpawnAmmo(T.position, T.rotation);
            }
            CanFire = false;
            Invoke("EnableFire", ReloadDelay);
        }

        //Look at mouse
        if(MouseLook)
        {
            Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));

            MousePosWorld = new Vector3(MousePosWorld.x, 0.0f, MousePosWorld.z);

            Vector3 LookDirection = MousePosWorld - transform.position;

            transform.localRotation = Quaternion.LookRotation(LookDirection.normalized, Vector3.up);
        }

    }//end FixedUpdate

    void EnableFire()
    {
        CanFire = true;
    }



}
