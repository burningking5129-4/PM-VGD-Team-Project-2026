using System.Collections;
using UnityEngine;


public class WeaponScript : MonoBehaviour
{
    public playerMovement player;
    public enemy enemy;

    [Header("Object Refrences")]
    public Rigidbody2D weaponRb;
    public GameObject weaponSlot;
    public GameObject projectile;
    public GameObject attackBox;
    public Transform firePoint;
    public Camera firingDirection;

    [Header("Meta Attributes")]
    public bool canAttack;
    public bool holdToAttack;
    public int weaponID;
    public string weaponName;
    public string weaponType;

    [Header("Weapon Stats")]
    public int weaponDamage;
    public float weaponStartup;
    public float weaponCooldown;

    [Header("Bow Stats")]
    public int arrows;
    public int maxArrows;
    public int arrowsDrawn;
    public int maxArrowsDrawn;
    public int arrowLifespan;
    public int arrowVelocity;
    public int arrowRegenRate;
    public bool loadingArrows;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();
        firingDirection = Camera.main;
    }
    
    public void Attack()
    {
        if (canAttack && weaponType == "Melee")
        {
            canAttack = false;
            attackBox.SetActive(true);
            StartCoroutine("Attacking");
        }

        if (canAttack && weaponType == "Ranged" && arrows > 0)
        {
            canAttack = false;

            arrows--;

            GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
            p.GetComponent<Rigidbody>().AddForce(firingDirection.transform.forward * arrowVelocity);
            Destroy(p, arrowLifespan);
        }
    }
    IEnumerator Attacking()
    {
        enemy.health -= weaponDamage;
        yield return new WaitForSeconds(0);

        if(weaponType == "Melee")
        {
            attackBox.SetActive(false);
        }

        canAttack = true;
    }

    // Note for self, add the hitboxes for the weapons and make the arrow
}
