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
        AutoRun(runSpeed);

        // �������� ������� ������ ������ (�� ��������� ������)
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            PerformJump(jumpForce);
        }

        // �������� ��������� ������ ������
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (Time.time - jumpStartTime < jumpHoldTime)
            {
                // ���������� ������ ������
                PerformJump(maxJumpForce);
            }
        }

        // ���� ������ ��������, ��������� ������
        if (Input.GetKey(KeyCode.W))
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.S) && isGrounded) // �������� "Fire2" �� ���� ������ ��� ����������
        {
            isSliding = true;
            Slide();
        }
        else
        {
            isSliding = false;
        }

        // �������� ������� ������ ������ �� ����� ����������
        if (Input.GetKey(KeyCode.W) && isSliding && isGrounded)
        {
            PerformJump(jumpForce + slideJumpBoost);
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

    void Slide()
    {
        // ��� ���������� �������� �������� �������������� ��������
        AutoRun(slideSpeed);
    }
}
