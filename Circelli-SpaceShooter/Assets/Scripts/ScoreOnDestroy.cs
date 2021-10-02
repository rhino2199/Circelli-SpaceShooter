/****
 * Created by: Ryan Circelli
 * Date Created: Sept 29, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Oct 2,2021
 * 
 * Description:Adds to score when object is destroyed
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDestroy : MonoBehaviour
{
    //Variables
    public int ScoreValue = 50;

    private void OnDestroy()
    {
        if (gameObject.GetComponent<EnemyAttack>().Destroyed)
        {
            GameManager.Score += ScoreValue;
            GameManager.ObjectDestroyed();
        }
    }
}
