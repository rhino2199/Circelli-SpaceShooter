/****
 * Created by: Ryan Circelli
 * Date Created: Sept 27, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 27,2021
 * 
 * Description:Player control Movements and Health and controls
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject destructionFX;
    public int MaxSpeed = 10;
    private float yPos = -6.6f;

    public static PlayerControl instance;
    Health H = null;
    public string HorzAxis = "Horizontal";
    public string FireAxis = "Fire1";
    private Rigidbody ThisBody = null;


    private void Awake()
    {
        H = GetComponent<Health>();
        ThisBody = GetComponent<Rigidbody>();
        if (instance == null)
            instance = this;
    }

    private void FixedUpdate()
    {
        float Horz = Input.GetAxis(HorzAxis);
        Vector3 MoveDirection = new Vector3(Horz, 0.0f , 0.0f);

        ThisBody.AddForce(MoveDirection.normalized * MaxSpeed);

        ThisBody.velocity = new Vector3(Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(ThisBody.velocity.y, -MaxSpeed, MaxSpeed), Mathf.Clamp(ThisBody.velocity.z, -MaxSpeed, MaxSpeed));

        //transform.position = Vector3.MoveTowards(transform.position, MoveDirection, 30 * Time.deltaTime);

    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        //Uses Health to give Player "3" lives
        H.HealthPoints -= 33;
        if (H.HealthPoints == 1)
        {
            Destruction();
        }
    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
}