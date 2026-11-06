using System;
using UnityEngine;


public class WeaponScript : MonoBehaviour
{
    public playerMovement player;

    public Rigidbody2D weaponRb;
    public GameObject weaponSlot;

    public int weaponDamage;
    public float weaponStartup;
    public float weaponCooldown;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
