/****
 * Created by: Ryan Circelli
 * Date Created: Sept 20, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Sept 20,2021
 * 
 * Description:Defines what object the game object will face towards
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObj : MonoBehaviour
{

    //Variables
    public Transform ObjToFace = null;
    public bool FacePlayer = false;


    private void Awake()
    {
        if (!FacePlayer) { return; }
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObj != null)
        {
            ObjToFace = PlayerObj.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ObjToFace == null) { return; }

        Vector3 DirToObj = ObjToFace.position - transform.position;

        if(DirToObj != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(DirToObj.normalized, Vector3.up);
        }

    }
}
