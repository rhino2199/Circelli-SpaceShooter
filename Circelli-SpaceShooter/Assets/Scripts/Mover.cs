/****
 * Created by: Ryan Circelli
 * Date Created: Sept 20, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 28,2021
 * 
 * Description:Continuosly moves the object
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Variables
    public float MaxSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, 1f, 0.0f) * MaxSpeed * Time.deltaTime;
    }//end update
}
