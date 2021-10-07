/****
 * Created by: Ryan Circelli
 * Date Created: Sept 29, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Oct 2,2021
 * 
 * Description:Game manager to control global game behaviours
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Damage done by enemy
    public int Damage = 100;
    //True if the enemy was destroyed by a player projectile
    public bool Destroyed = true;
    private bool Attacking = false;
    //Upperbound of random attack time range
    public float AttackRange = 3.0f;
    //Speed of attacking enemies
    public int AttackSpeed = 5;
    bool right;

    private void Start()
    {
        right = (Random.Range(1, 3) % 2 == 0);
    }

    public void Awake()
    {
        Invoke("Attack", Random.Range(1f, AttackRange));
    }

    // Update is called once per frame
    void Update()
    {
        //If attacking enemy moves down towards the player
        //Could Add enemy type that gtoes directly toward the player
        //Could make health for enemies that get past the player
        if (Attacking)
        {
            transform.position += new Vector3(0.0f, -1, 0.0f) * AttackSpeed * Time.deltaTime;
        } 
        else
        {
            if(transform.position.x < -4.5)
            {
                right = true;
            }
            if (transform.position.x > 4.5)
            {
                right = false;
            }
            if (right)
            {
                transform.position += new Vector3(1, 0.0f, 0.0f) * AttackSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(-1, 0.0f, 0.0f) * AttackSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health h = other.GetComponent<Health>();
        if (other.tag == "Player" || other.tag == "Bounds")
        {
            if(other.tag == "Player")
            {//Do Damage to player
                if (h == null) { return; }
                h.HealthPoints -= Damage; 
                GameManager.ObjectDestroyed();
            }
            //destory this enemy
            h = gameObject.GetComponent<Health>();
            if (h == null) { return; }
            Destroyed = false;
            h.HealthPoints -= Damage;
           
        }
        
    }

    //Sets the enemy to attacking mode
    void Attack()
    {
        Attacking = true;
    }
}
