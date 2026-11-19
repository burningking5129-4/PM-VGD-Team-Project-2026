using System.Collections;
using UnityEngine;

public class meleeWeapon : MonoBehaviour
{
    public playerMovement player;

    [Header("Object Refrences")]
    public Rigidbody2D weaponRb;
    public GameObject weaponSlot;
    public GameObject attackBox;

    [Header("Meta Attributes")]
    public bool canAttack;
    public bool holdToAttack;
    public int weaponID;
    public string weaponName;
    public string weaponType;

    [Header("Weapon Stats")]
    public int weaponDamage;
    public float weaponStartup;
    public float weaponAttackTime;
    public float weaponCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();

        attackBox = GameObject.FindGameObjectWithTag("attackBox");
        attackBox.SetActive(false);
    }

    public void meleeAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            attackBox.SetActive(true);
            StartCoroutine("Attacking");
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(weaponAttackTime);
            attackBox.SetActive(false);
            canAttack = true;
    }
}
