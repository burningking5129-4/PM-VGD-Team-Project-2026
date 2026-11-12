using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //player movement variables \ speed doesnt change in script, must change in inspector
    public float speed = 3.5f;
    public float hp = 100;
    public float maxhp = 100;

    public string foundWeapon;
    public string[] weaponsKnown;
    public string[] gate;
    public string findingGate;
    public string weaponHeld;

    public bool isAttacking = false;
    public bool canAttack = false;

    public GameObject currentWeaponObj;
    public GameObject findingWeapon;
    public Collider2D currentWeaponCol;

    public WeaponScript weapon;

    Transform weaponSlot;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    
    //initializes variables
    void Start()
    {
        weaponSlot = transform.GetChild(0);
        rb.GetComponent<Rigidbody2D>();
        weaponsKnown = new string[3];
        gate = new string[3];
        weaponsKnown[0] = "sword";
        weaponsKnown[1] = "spear";
        weaponsKnown[2] = "bow";
        gate[0] = "fireGate";
        gate[1] = "waterGate";
        gate[2] = "natureGate";
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
            if(findingWeapon.tag  == weaponsKnown[i])
            {
                foundWeapon = weaponsKnown[i];
            }
        }
        return foundWeapon;
    }

    public string findGate()
    {
        for (int i = 0; i < gate.Length; i++)
        {
            if (findingGate == gate[i])
            {
                findingGate = gate[i]; ;
            }
        }
        return findingGate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        findingWeapon = collision.gameObject;
        if (currentWeaponObj == null)
        {
            if (collision.gameObject.tag == findWeaponName())
            {
                Debug.Log("collided");
                collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position, new Quaternion(0, 0, -90f, 90));
                if (findWeaponName() == "spear")
                {
                    rb.rotation = 90;

                }

                weaponHeld = collision.gameObject.tag;

                collision.gameObject.transform.SetParent(weaponSlot);

                collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;

                collision.collider.enabled = false;

                collision.rigidbody.linearVelocity = Vector3.zero;

                currentWeaponObj = collision.gameObject;
                currentWeaponCol = currentWeaponObj.GetComponent<Collider2D>();
                weapon = currentWeaponObj.GetComponent<WeaponScript>();
                canAttack = true;
            }
        }
                findingGate = collision.gameObject.tag;
                if (collision.gameObject.tag == findGate())
                {
                    if (findGate() == "fireGate")
                    {
                        SceneManager.LoadScene(1);
                    }
                    else if (findGate() == "waterGate")
                    {
                        SceneManager.LoadScene(3);
                    }
                    else if (findGate() == "natureGate")
                    {
                        SceneManager.LoadScene(2);
                    }
                    if (collision.gameObject.tag == "dungonExit")
                    {
                        SceneManager.LoadScene(0);
                    }
                }
        
    }

    public void Drop()
    {
        if (currentWeaponObj != null)
        {
            currentWeaponCol.enabled = true;
            weaponSlot.DetachChildren();
            weapon.weaponRb.linearVelocity = Vector2.zero;
            currentWeaponObj = null;
        }
    }
}

  