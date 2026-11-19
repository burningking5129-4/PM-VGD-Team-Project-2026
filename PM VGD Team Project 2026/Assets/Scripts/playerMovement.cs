using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //player movement variables \ speed doesnt change in script, must change in inspector
    public float speed = 3.5f;
    public float hp;
    public float maxhp;

    public int roomNum;

    public string foundWeapon;
    public string[] weaponsKnown;
    public string[] gate;
    public string findingGate;
    public string weaponHeld;
    public string weaponType;

    public bool isAttacking = false;
    public bool canAttack = false;

    public GameObject loadWeapon;
    public GameObject currentWeaponObj;
    public GameObject findingWeapon;
    public GameObject spawnPoint;
    public Collider2D currentWeaponCol;

    public gameManager gm;
    public WeaponScript weapon;

    Transform weaponSlot;
    public Rigidbody2D rb;
    public Vector2 moveInput;

    //initializes variables
    void Start()
    {
        hp = 100;
        maxhp = 100;
        gameObject.transform.position = spawnPoint.transform.position;
        DontDestroyOnLoad(gm);
        DontDestroyOnLoad(gameObject);
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

        rb.rotation = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) * Mathf.Rad2Deg;
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

    public void attack()
    {
        Debug.Log(weapon.weaponType);
        Debug.Log(canAttack);
        weapon.Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        findingWeapon = collision.gameObject;
        if (currentWeaponObj == null)
        {
            if (collision.gameObject.tag == findWeaponName())
            {
                collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position, new Quaternion(0, 0, -90f, 90));
                if (findWeaponName() == "spear" || findWeaponName() == "sword")
                {
                    weaponType = "melee";
                } else if(findWeaponName() == "bow") 
                {
                    weaponType = "ranged";
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
                        SceneManager.LoadScene(4);
                    }
                }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "roomOne")
        {
            roomNum = 1;
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

  