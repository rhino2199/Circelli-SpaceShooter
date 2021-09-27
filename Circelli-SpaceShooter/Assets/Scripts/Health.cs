/****
 * Created by: Ryan Circelli
 * Date Created: Sept 20, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 20,2021
 * 
 * Description:Health system for any game object
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Variables
    [SerializeField] private float _HealthPoints = 100f;
    public bool DestroyOnDeath = true;
    public GameObject DeathParticlesPrefab = null;


    public float HealthPoints { 
        get { return _HealthPoints; }
        set { _HealthPoints = value;
            if (_HealthPoints <= 0)
            {
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);

                if (DeathParticlesPrefab != null)
                {
                    Instantiate(DeathParticlesPrefab, transform.position, transform.rotation);
                }

                if (DestroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }
        }
    }//end public health points

    // Update is called once per frame
    void Update()
    {
       //Debug health test
      /* if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthPoints = 0;
        }//end debug*/
    }
}
