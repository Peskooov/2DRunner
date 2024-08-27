using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 0.3f; // ���� �������� ������
    public float runSpeed = 5f; // ���� �������� ������
    public float maxJumpForce = 0.6f; // ���� ������ ��� ����������� ������
    public float jumpHoldTime = 0.5f; // �����, � ������� �������� ������ ����� ������������ ��� ���������� ������ ������
    public float slideSpeed = 10f; // �������� ����������
    public float slideJumpBoost = 1f; // �������������� ���� ������ ��� ����������
    private bool isGrounded; // �������� �� ����� �� ��������
    private bool isJumping; // ��������, ������ �� ������ ������
    private bool isSliding; // ��������, ������� �� ����������
    private float jumpStartTime; // ����� ������ ������

    private float checkTime; 

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
            isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void Update()
    {
        Debug.Log(Vector3.up);
        AutoRun(runSpeed);

        // �������� ������� ������ ������ (�� ��������� ������)
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && isGrounded)
        {
            
            jumpStartTime = Time.time; 
            PerformJump(jumpForce);
            isJumping = true;
        }

        // �������� ��������� ������ ������
        if (Input.GetKey(KeyCode.W) && isJumping && !isGrounded)
        {
            if (Time.time - jumpStartTime > jumpHoldTime)
                PerformJump(maxJumpForce);
            // ���������� ������ ������

            //   checkTime = Time.time;

            // if(Time.time - checkTime > jumpHoldTime)
            // isJumping = false;
            else isJumping = false;
            
        }

        // ���� ������ ��������, ��������� ������
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.S) && isGrounded) // �������� "Fire2" �� ���� ������ ��� ����������
        {
            isSliding = true;
            AutoRun(slideSpeed);
        }
        else
        {
            isSliding = false;
        }

        // �������� ������� ������ ������ �� ����� ����������
        if (Input.GetKey(KeyCode.W) && isSliding && isGrounded)
        {
            PerformJump(jumpForce + slideJumpBoost);

            isGrounded = false;
            isSliding = false;
        }
    }

    public void AutoRun(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void PerformJump(float force)
    {       
            // ���������� ���� ������
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // ��������� ������������ ��������
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);    
    }
}
