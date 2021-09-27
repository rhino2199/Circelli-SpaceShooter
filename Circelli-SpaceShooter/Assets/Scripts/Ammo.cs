/****
 * Created by: Ryan Circelli
 * Date Created: Sept 22, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 22,2021
 * 
 * Description:Controls Game ammo
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //Variables
    public float Damage = 100f;
    public float Lifetime = 2f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Die", Lifetime);
    }//end OnEnable

    private void OnTriggerEnter(Collider other)
    {
        Health H = other.gameObject.GetComponent<Health>();
        if(H == null) { return; }
        H.HealthPoints -= Damage;
        Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

}
