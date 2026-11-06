using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //player movement variables \ speed doesnt change in script, must change in inspector
    public float speed = 3.5f;

    public string foundWeapon;
    public string[] weaponsKnown;

    public bool isAttacking = false;
    public bool canAttack = false;

    public GameObject currentWeaponObj;

    public WeaponScript weapon;

    Transform weaponSlot;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    
    //initializes the rigidbody and identifies it
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        weaponsKnown = new string[3];
        weaponsKnown[0] = "sword";
        weaponsKnown[1] = "spear";
        weaponsKnown[2] = "bow";
    }

   //defines the velocity
    void Update()
    {
        rb.linearVelocity = moveInput * speed;
    }
    
    //moves the player
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public string findWeaponName()
    {
        for(int i = 0; i < weaponsKnown.Length; i++)
        {
            if(gameObject.tag  == weaponsKnown[i])
            {
                foundWeapon = weaponsKnown[i];
            }
        }
        return foundWeapon;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == findWeaponName())
        {
            collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position,
                                                        new Quaternion(0, 0, -90f, 90));

            collision.gameObject.transform.SetParent(weaponSlot);

            collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            collision.rigidbody.simulated = false;

            collision.collider.enabled = false;

            currentWeaponObj = collision.gameObject;
            canAttack = true;
        }
    }
}
