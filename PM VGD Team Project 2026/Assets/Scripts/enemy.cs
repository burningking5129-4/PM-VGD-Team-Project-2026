using System.Collections;

using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2f;
    public float strength = 10f;
    public float attackCooldown = 1.5f;

    public bool isAttacking = false;

    public playerMovement player; 
    Rigidbody2D rb;
    Transform target;
    Vector2 moveAim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        moveAim = direction;
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveAim.x, moveAim.y) * speed;
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
