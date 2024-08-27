using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 0.3f; // Сила обычного прыжка
    public float runSpeed = 5f; // Сила обычного прыжка
    public float maxJumpForce = 0.6f; // Сила прыжка при удерживании кнопки
    public float jumpHoldTime = 0.5f; // Время, в течение которого кнопка может удерживаться для увеличения высоты прыжка
    public float slideSpeed = 10f; // Скорость скольжения
    public float slideJumpBoost = 1f; // Дополнительная сила прыжка при скольжении
    private bool isGrounded; // Проверка на земле ли персонаж
    private bool isJumping; // Проверка, нажата ли кнопка прыжка
    private bool isSliding; // Проверка, активен ли скольжение
    private float jumpStartTime; // Время начала прыжка

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

        // Проверка нажатия кнопки прыжка (по умолчанию пробел)
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            PerformJump(jumpForce);
        }

        // Проверка удержания кнопки прыжка
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (Time.time - jumpStartTime < jumpHoldTime)
            {
                // Увеличение высоты прыжка
                PerformJump(maxJumpForce);
            }
        }

        // Если кнопка отпущена, завершить прыжок
        if (Input.GetKey(KeyCode.W))
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.S) && isGrounded) // Замените "Fire2" на вашу кнопку для скольжения
        {
            isSliding = true;
            Slide();
        }
        else
        {
            isSliding = false;
        }

        // Проверка нажатия кнопки прыжка во время скольжения
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
        // Добавление силы прыжка
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Обнуление вертикальной скорости
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void Slide()
    {
        // При скольжении персонаж получает дополнительную скорость
        AutoRun(slideSpeed);
    }
}
