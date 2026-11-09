using UnityEngine;

public class enemy : MonoBehaviour
{
    public playerMovement player;
    public WeaponScript weapon;
    public gameManager gm;

    public float detectionRange = 5;
    
    public int health = 50;
       
    public bool isAttacking = false;
    public bool inRoom = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
