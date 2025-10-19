using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Header("Movement Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int baseDoubleJumps;
    private Vector2 movement;
    private int doubleJumps;

    [Header("Gun Variables")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject GunSpawnPoint;
    [SerializeField] private Transform rotationPoint;
    private GameObject gunObject;
    private Camera cam;
    private Vector3 mousePos;
    private float cooldownTimer = 0.5f;

    private int maxHealth = 3;
    private int currentHealth;
    public Slider healthBar;
    public int damageAmount = 1;


    void Awake() => cam = Camera.main;

    public bool isGrounded
    {
        get => Physics2D.OverlapCircle(groundChecker.position, groundCheckDistance, groundLayer);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");

        if (movement.x > 0)
        {
            sr.flipX = true;
        }
        else if (movement.x < 0)
        {
            sr.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && doubleJumps > 0)
        {
            rb.AddForceY(jumpForce);
            if (doubleJumps > 0 && !isGrounded) { doubleJumps--; }
        }
        if (isGrounded) { doubleJumps = baseDoubleJumps; }


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotationPoint.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= 0.5f)
        {
            gunObject = Instantiate(gun, GunSpawnPoint.transform.position, rotationPoint.rotation, this.transform);
            if (rotation.x < 0f) { gunObject.GetComponent<SpriteRenderer>().flipY = true; }
            cooldownTimer = 0;
        }
        cooldownTimer += Time.deltaTime;

        // checking for game over
         if (currentHealth <= 0)
         {
             currentHealth = 0;
             SceneManager.LoadScene("GameOverScreen");
         }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShieldMechanics shield = GetComponent<ShieldMechanics>();
        if(shield != null && shield.IsShieldActive)
        {
            return;
        }

        if (other.gameObject.CompareTag("EnemyBullet") && currentHealth > 0)
        {
            Destroy(other.gameObject);
            currentHealth -= damageAmount;
        
            //UI change
            healthBar.value = currentHealth;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocityY);
    }
    
    
}
