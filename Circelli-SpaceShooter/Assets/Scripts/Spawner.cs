/****
 * Created by: Ryan Circelli
 * Date Created: Sept 20, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Oct 2,2021
 * 
 * Description:Spawns Enemies
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Variables

    public float Interval = 1.5f;
    public GameObject ObjToSpawn = null;
    private Transform Origin = null;
    //Times to increace the spawning rate of enemies as the level progesses
    public float[] SpawnIncreaceTimes = new float[10];
    public int EnemyPerSpawn = 2;
  



    private void Awake()
    {
        Origin = transform;
    }

    public void GameOver()
    {
        CancelInvoke();
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, Interval);
    }

    //Spawns EnemyPerSpawn enemies at random places at the top of the screen
    void Spawn()
    {
        for (int i = 0; i < EnemyPerSpawn; i++)
        {
            float xPos = Random.Range(-4.5f, 4.6f);
            float yPos = Random.Range(3.5f, 5.6f);
            //Ask about searching for other enemies in potential spawn postition
            Vector3 SpawnPos = new Vector3(xPos, yPos, 0.0f);
            Instantiate(ObjToSpawn, SpawnPos, Quaternion.identity);
        }
    }
  
}
