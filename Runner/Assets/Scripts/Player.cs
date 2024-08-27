using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputController controller;

    [Header("Player")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float holdJumpForce = 20f;
    [SerializeField] private float holdTime = 0.2f;

    public float JumpForce => jumpForce;
    public float HoldJumpForce => holdJumpForce;
    public float HoldTime => holdTime;
   
    /*
    [Header("Raycast")]
    [SerializeField] private float rayLength;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private Vector3 boxSize;

    */

    private Rigidbody rb;
    public Rigidbody rbRigidbody =>rb;

    private bool isGround;
    public bool IsGround => isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.up = Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
            isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
    }

    public void HoldJump()
    {
        rb.AddForce(Vector3.up * holdJumpForce, ForceMode.Impulse);
    }

    public void AutoRun()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
}