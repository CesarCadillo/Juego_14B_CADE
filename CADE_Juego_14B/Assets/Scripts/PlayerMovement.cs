using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce;
    Rigidbody2D rb2d;
    bool isGrounded;
    Animator animator;
    [SerializeField] GameObject Top, Bottom;
    [SerializeField] TextMeshProUGUI puntajeTextFinal;
    [SerializeField] GameObject EndPanel;

    [Header("UI Buttons")]
    [SerializeField] Button jumpButton;
    [SerializeField] Button duckButton;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpButton.gameObject.AddComponent<JumpButtonHandler>().Init(this);
        duckButton.gameObject.AddComponent<DuckButtonHandler>().Init(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
        if (Input.GetKeyDown(KeyCode.S)) Duck();
        if (Input.GetKeyUp(KeyCode.S)) Stand();
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
            Jump();
    }

    public void OnDuckButtonDown()
    {
        animator.SetBool("Up", false);
        animator.SetBool("Down", true);
        Duck();
    }

    public void OnDuckButtonUp()
    {
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        Stand();
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("Down", false);
    }

    void Duck()
    {
        Bottom.SetActive(true);
        Top.SetActive(false);
    }

    void Stand()
    {
        Top.SetActive(true);
        Bottom.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Up", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("Up", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int p = FindFirstObjectByType<Puntaje>().GetPuntaje();
            puntajeTextFinal.text = "" + p;
            EndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler
{
    PlayerMovement player;

    public void Init(PlayerMovement p) => player = p;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.OnJumpButtonPressed();
    }
}

public class DuckButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerMovement player;

    public void Init(PlayerMovement p) => player = p;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.OnDuckButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.OnDuckButtonUp();
    }
}
