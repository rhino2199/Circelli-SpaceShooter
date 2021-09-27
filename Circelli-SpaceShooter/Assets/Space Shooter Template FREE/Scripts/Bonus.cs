using UnityEngine;

public class Bonus : MonoBehaviour {

    //when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            if (PlayerShoot.instance.weaponPower < PlayerShoot.instance.maxweaponPower)
            {
                PlayerShoot.instance.weaponPower++;
            }
            Destroy(gameObject);
        }
    }
}
