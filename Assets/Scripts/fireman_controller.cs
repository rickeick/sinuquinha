using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fireman_controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isJumping;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float fallMultiplier = 4f; // Aumenta a velocidade da queda
    public float lowJumpMultiplier = 3f; // Ajusta saltos mais curtos
    public int helpdNPCs;
    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float cameraHeightOffset = 3.0f;
    public float cameraSmoothSpeed = 0.1f;
    public float cameraSizeIncrease = 1f;
    public bool useOrthographic = true;

    private Vector3 cameraTargetPosition;
    private Camera mainCamera;

    void Start()
    {
        moveSpeed = 6f;
        runSpeed = 9f; // Definição da velocidade de corrida
        jumpForce = 10f;
        helpdNPCs = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;

        if (cameraTransform != null)
        {
            mainCamera = cameraTransform.GetComponent<Camera>();
            cameraTargetPosition = cameraTransform.position;
            AdjustCameraSize();
        }
    }

    void Update()
    {
        if (animator.GetInteger("life") > 0)
        {
            // Movimento horizontal
            float horizontalInput = Input.GetAxis("Horizontal");
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // Verifica se está correndo
            float currentSpeed = isRunning ? runSpeed : moveSpeed;

            if (horizontalInput != 0)
            {
                animator.SetFloat("speed", Mathf.Abs(currentSpeed));
                animator.SetBool("isrunning", isRunning); // Atualiza o parâmetro no Animator
                transform.localScale = new Vector3(horizontalInput > 0 ? -1 : 1, 1f, 1f);
                transform.Translate(Vector3.right * currentSpeed * horizontalInput * Time.deltaTime);
            }
            else
            {
                animator.SetFloat("speed", 0f);
                animator.SetBool("isrunning", false); // Para de correr quando parado
            }

            // Pulo
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("isjumping", true);
                isJumping = true;
            }
        }

        // Ajustar velocidade de queda
        if (rb.velocity.y < 0) // Descendo
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) // Subida com botão solto
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        UpdateCameraPosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isjumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            int currentLife = animator.GetInteger("life");
            currentLife -= 7;

            if (currentLife <= 0)
            {
                currentLife = 0;
                animator.SetTrigger("die"); // Gatilho para animação de morte
                Debug.Log("Personagem morreu!");
                SceneManager.LoadScene("GameOver");
                // Adicionar lógica para reiniciar o jogo ou exibir tela de game over
            }

            animator.SetInteger("life", currentLife); // Atualiza a vida no Animator
            Debug.Log("Vida restante: " + currentLife);

            // Ativar a animação de dano
            StartCoroutine(TakeDamageAnimation());
        }

        if (collision.CompareTag("NPC"))
        {
            StartCoroutine(HelpNPC());
        }
    }

    /// <summary>
    /// Ativa a animação de dano e desativa após 1 segundo
    /// </summary>
    IEnumerator TakeDamageAnimation()
    {
        animator.SetBool("hit", true); // Ativa animação de dano
        yield return new WaitForSeconds(1f); // Espera 1 segundo
        animator.SetBool("hit", false); // Desativa animação de dano
    }

    /// <summary>
    /// Ativa a animação de ajudar um NPC e desativa após 5 segundos
    /// </summary>
    IEnumerator HelpNPC()
    {
        animator.SetBool("helping", true); // Ativa animação de ajudar NPC
        Debug.Log("🔥 Ajudando NPC...");
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        animator.SetBool("helping", false); // Desativa animação de ajudar NPC
        helpdNPCs += 1;
        Debug.Log($"✅ NPC n° {helpdNPCs} ajudado!");
        if(helpdNPCs == 3){
            SceneManager.LoadScene("FinishedGame");
        }
    }

    private void AdjustCameraSize()
    {
        if (mainCamera != null)
        {
            if (useOrthographic && mainCamera.orthographic)
            {
                mainCamera.orthographicSize += cameraSizeIncrease;
            }
            else if (!useOrthographic && !mainCamera.orthographic)
            {
                mainCamera.fieldOfView += cameraSizeIncrease;
            }
        }
    }

    private void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            cameraTargetPosition = new Vector3(
                transform.position.x,
                transform.position.y + cameraHeightOffset,
                cameraTransform.position.z
            );

            cameraTransform.position = Vector3.Lerp(
                cameraTransform.position,
                cameraTargetPosition,
                cameraSmoothSpeed
            );
        }
    }
}
