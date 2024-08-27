using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    public event UnityAction RunEvent;
    public event UnityAction JumpEvent;
    public event UnityAction SlidingEvent;

    [SerializeField] private Player player;

    private float moveHorizontal;
    public float MoveHorizontal => moveHorizontal;

    private float jumpStartTime;
    private float maxHoldTime=10;

    private void Update()
    {


       // player.AutoRun();
        RunEvent?.Invoke();

        // ���� ������ ������ ������ � �������� ��������� �� �����
        if (Input.GetButtonDown("Jump") && player.IsGround)
        {
            jumpStartTime = Time.time;
            player.rbRigidbody.velocity = new Vector3(player.rbRigidbody.velocity.x, 0f, player.rbRigidbody.velocity.z); // ����� ������������ �������� ����� �������
        }

        // ���� ������ ������ ������������ � �������� ��������� �� �����
        if (Input.GetButton("Jump") && player.IsGround)
        {
            float timeHeld = Time.time - jumpStartTime;
            if (timeHeld < maxHoldTime)
            {
                player.rbRigidbody.AddForce(Vector3.up * player.HoldJumpForce * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        // ���� ������ ������ ��������
        if (Input.GetButtonUp("Jump") && player.IsGround)
        {
            float timeHeld = Time.time - jumpStartTime;

            if (timeHeld <= player.HoldTime)
            {
                // ������� ������ ��� ��������������� ������� ������
                player.rbRigidbody.AddForce(Vector3.up * player.JumpForce, ForceMode.Impulse);
            }
        }

        if (!player.IsGround)
        {
            JumpEvent?.Invoke();
        }

       
    }
}