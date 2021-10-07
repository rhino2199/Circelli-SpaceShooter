/****
 * Created by: Ryan Circelli
 * Date Created: Sept 26, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Oct 2,2021
 * 
 * Description:Player control Movements and Health and controls
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
}

public class PlayerShoot : MonoBehaviour
{

    
    public GameObject projectileObject;
    //time for a new shot
    [HideInInspector] public float nextFire;

    public int weaponPower = 1;

    public int PoolSize = 100;
    public Queue<Transform> AmmoQueue = new Queue<Transform>();
    private GameObject[] AmmoArray;

    public Guns guns;
    [HideInInspector] public int maxweaponPower = 4;
    public static PlayerShoot instance;
    bool CanFire = true;
    public float ReloadTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        //Creates ammo pool
        AmmoArray = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            AmmoArray[i] = Instantiate(projectileObject, Vector3.zero, Quaternion.identity, transform) as GameObject;
            Transform ObjTransform = AmmoArray[i].transform;

            AmmoQueue.Enqueue(ObjTransform);
            AmmoArray[i].SetActive(false);
        }
    }
    private void Start() // Written by asset provider
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();

    }

    //Checks for shoot button
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanFire)
        {
            MakeAShot();
            CanFire = false;
            Invoke("Reload", ReloadTime);
        }
    }

    void Reload()
    {
        CanFire = true;
    }

    //method for a shot // Written by asset provider
    void MakeAShot()
    {
        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                break;
            case 4:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break;
        }
    }

    //Spawn instance of ammo from pool
    public static Transform SpawnAmmo(Vector3 Position, Quaternion Rotation)
    {
        Transform SpawnedAmmo = instance.AmmoQueue.Dequeue();
        SpawnedAmmo.gameObject.SetActive(true);
        SpawnedAmmo.position = Position;
        SpawnedAmmo.localRotation = Rotation;
        instance.AmmoQueue.Enqueue(SpawnedAmmo);
        return SpawnedAmmo;
    }

    //Spawns ammo and plays shooting sound
    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        SpawnAmmo(pos, Quaternion.Euler(rot));
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
    }
}
