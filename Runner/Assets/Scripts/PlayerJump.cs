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

        // Проверка нажатия кнопки прыжка (по умолчанию пробел)
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && isGrounded)
        {
            
            jumpStartTime = Time.time; 
            PerformJump(jumpForce);
            isJumping = true;
        }

        // Проверка удержания кнопки прыжка
        if (Input.GetKey(KeyCode.W) && isJumping && !isGrounded)
        {
            if (Time.time - jumpStartTime > jumpHoldTime)
                PerformJump(maxJumpForce);
            // Увеличение высоты прыжка

            //   checkTime = Time.time;

            // if(Time.time - checkTime > jumpHoldTime)
            // isJumping = false;
            else isJumping = false;
            
        }

        // Если кнопка отпущена, завершить прыжок
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.S) && isGrounded) // Замените "Fire2" на вашу кнопку для скольжения
        {
            isSliding = true;
            AutoRun(slideSpeed);
        }
        else
        {
            isSliding = false;
        }

        // Проверка нажатия кнопки прыжка во время скольжения
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
            // Добавление силы прыжка
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Обнуление вертикальной скорости
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);    
    }
}
