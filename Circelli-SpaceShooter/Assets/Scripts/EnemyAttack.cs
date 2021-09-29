using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        Invoke("Attack", Random.Range(1f, 6f));
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            transform.position += new Vector3(0.0f, -1, 0.0f) * 5 * Time.deltaTime;
        } 
        else
        {
            //move from side to side
        }
    }

    void Attack()
    {
        attacking = true;
    }
}
