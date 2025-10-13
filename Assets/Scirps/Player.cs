using System;
using Unity.Jobs;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
  [SerializeField]  private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameControl gameControl;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    } 
    void Start()
    {
        currentHp=maxHp;
        updateHpBar();
    }

   
    void Update()
    {
        movePlayer();
        if (Input.GetKeyDown(KeyCode.Escape)) { 
        
        gameControl.GamePause(); 
        }
    }
    void movePlayer ()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("isRun", true);


        } else
        {
            animator.SetBool("isRun", false);
        }
    }


    public void takeDame(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
        {
            Die();
        }
        updateHpBar();
    }
    public void heal(float heal)
    {
        if (currentHp < maxHp)
        {
            Console.WriteLine(heal);
            currentHp = currentHp + heal;
            updateHpBar();
           

        }
    }
    public void updateHpBar()
    {
        if (hpBar != null)
        {
            {
                hpBar.fillAmount = currentHp / maxHp;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        gameControl.GameOver();
    }
}
