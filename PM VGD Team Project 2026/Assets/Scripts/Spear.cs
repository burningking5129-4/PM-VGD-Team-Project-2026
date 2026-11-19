using UnityEngine;

public class Spear : WeaponScript 
{
    
    private void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();
        firingDirection = Camera.main;
        attackBox = GameObject.FindGameObjectWithTag("attackBox");
    }

}
