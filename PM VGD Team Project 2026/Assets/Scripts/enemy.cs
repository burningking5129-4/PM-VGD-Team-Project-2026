using System.Collections;

using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health;
    public int roomNumber;

    public float speed = 2f;
    public float strength = 10f;
    public float attackCooldown = 1.5f;

    public bool isAttacking = false;

    public playerMovement player;
    public WeaponScript weapon;
    public Spear Spear;

    Rigidbody2D rb;
    Transform destination;
    Vector2 moveAim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            // Finds the player automatically in the scene
            player = FindAnyObjectByType<playerMovement>();
        }
        roomNumber = 0;
        rb = GetComponent<Rigidbody2D>();
        destination = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (roomNumber == player.roomNum)
        {
            Vector3 direction = (destination.position - transform.position);
            moveAim = direction;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("update did it");
        }
    }

    public void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("fixedupdate did it");
        }
        if (roomNumber == player.roomNum)
        {
           rb.linearVelocity = new Vector2(moveAim.x, moveAim.y) * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            isAttacking = true;
            player.hp -= strength;
            StartCoroutine("damageCooldown");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "roomOne")
        {
            roomNumber = 1;
            Debug.Log("enemy is in room one");
        }
        if (other.gameObject.tag == "attackBox" || other.gameObject.tag == "arrow")
        {
            health -= Spear.weaponDamage;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopCoroutine("damageCooldown");
        speed = 2;
    }

    IEnumerator damageCooldown()
    {
        speed = 0;
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);

        player.hp -= strength;
        isAttacking = true;
        StartCoroutine("damageCooldown");

    }
}
