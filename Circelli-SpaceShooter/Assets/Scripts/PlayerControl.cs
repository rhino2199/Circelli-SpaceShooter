/****
 * Created by: Ryan Circelli
 * Date Created: Sept 27, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 28,2021
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
    //Higher the speed the slower the player moves
    public float Speed = 4.0f;

    public static PlayerControl instance;
    Health H = null;
    public string HorzAxis = "Horizontal";
    private Rigidbody ThisBody = null;
    private Vector3 CurrentPos;


    private void Awake()
    {
        H = GetComponent<Health>();
        ThisBody = GetComponent<Rigidbody>();
        CurrentPos = ThisBody.position;
        if (instance == null)
            instance = this;
    }

    private void FixedUpdate()
    {
        float Horz = Input.GetAxis(HorzAxis);
        Vector3 MoveDirection = new Vector3(Horz, 0.0f, 0.0f);
        CurrentPos = ThisBody.position;
        transform.position = Vector3.MoveTowards(CurrentPos, MoveDirection/Speed + CurrentPos, 30 * Time.deltaTime);
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        //Uses Health to give Player "3" lives
        H.HealthPoints -= 100;
        if (H.HealthPoints == 0)
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