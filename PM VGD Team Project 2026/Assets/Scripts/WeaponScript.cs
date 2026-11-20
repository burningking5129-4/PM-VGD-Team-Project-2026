using System.Collections;
using Unity.VisualScripting;
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
    public bool overlapping;
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
        enemy = gameObject.GetComponent<enemy>();
    }
    
    public void Attack()
    {
        if (player.canAttack == true && weaponType == "melee")
        {
            player.canAttack = false;
            player.isAttacking = true;
            StartCoroutine("Attacking");
            StartCoroutine("attackLength");
        }

        if (player.canAttack && weaponType == "ranged")
        {
            player.canAttack = false;

            arrows--;

            GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
            p.GetComponent<Rigidbody>().AddForce(firingDirection.transform.forward * arrowVelocity);
            Destroy(p, arrowLifespan);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            overlapping = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            overlapping = false;
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(weaponCooldown);

        player.canAttack = true;
    }

    IEnumerator attackLength()
    {
        yield return new WaitForSeconds(0.1f);
        player.isAttacking = false;
    }

    // Note for self, add the hitboxes for the weapons and make the arrow
}
