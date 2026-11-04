using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //player movement variables \ speed doesnt change in script, must change in inspector
    public float speed = 3.5f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    
    //initializes the rigidbody and identifies it
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
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
}
