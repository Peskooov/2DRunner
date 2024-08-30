using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction RunEvent;
    public event UnityAction JumpEvent;
    public event UnityAction SlideEvent;

    [SerializeField] private float runSpeed = 5f; //�������� ����
    [SerializeField] private float jumpForce = 3f; // ���� ������
    [SerializeField] private float jumpControlTime = 0.6f; // ������������ ����� ������
    [SerializeField] private float slideSpeed = 10f; // �������� ����������
    [SerializeField] private float slideControlTime = 0.6f; // ������������ ����� ����������

    [SerializeField] private Transform rayTransform; // ������ ������, �������� �� ������, ���������� ����� ������
    [SerializeField] private float raycastDistance = 1.5f; // ����������, �� ������� ����� ������� �������

    private bool isGrounded; // �������� �� ����� �� ��������
    private bool isJumping; // ��������, ������ �� ������ ������
    private bool isSliding; // ��������, ������� �� ����������

    private float jumpTime = 0;
    private float slideTime = 0;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.up = Vector3.up;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        if (IsBarrier() == false)
            Slide();

        Jump();
    }

    private void Run(float speed)
    {
        RunEvent?.Invoke();

        rb.velocity = new Vector3(0f, rb.velocity.y, speed);
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("press S");
            if (isGrounded)
                isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (isSliding)
        {
            SlideEvent?.Invoke();
            if ((slideTime += Time.deltaTime) < slideControlTime)
            {
                Run(slideSpeed);
            }
        }
        else
        {
            slideTime = 0;
        }
        Run(runSpeed);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
                isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (isJumping)
        {
            JumpEvent?.Invoke();
            if ((jumpTime += Time.deltaTime) < jumpControlTime)
            {
                rb.velocity = new Vector3(0f, jumpForce / (jumpTime * 10), rb.velocity.z);
                //rb.AddForce(Vector3.up * jumpForce / (jumpTime * 10));
            }
        }
        else
        {
            jumpTime = 0;
        }
    }

    private bool IsBarrier()
    {
        // ��������� �������
        RaycastHit hit;

#if UNITY_EDITOR
        // ������������ �������� (��� �������)
        Debug.DrawRay(rayTransform.transform.position, transform.forward * raycastDistance, Color.red);
#endif

        if (Physics.Raycast(rayTransform.transform.position, transform.forward, out hit, raycastDistance))
        {
            // ���� ������� ����� � ���������
            Debug.Log("Hit object: " + hit.collider.name);
            return true;
        }
        else
        {
            // ���� ������� �� ����� � ���������
            Debug.Log("No hit");
            return false;
        }
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
}