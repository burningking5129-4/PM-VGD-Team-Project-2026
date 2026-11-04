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

    public string[] weaponsKnown = new string[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();
        weaponsKnown[0] = "sword";
        weaponsKnown[1] = "spear";
        weaponsKnown[2] = "bow";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
