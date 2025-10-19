using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    private Transform target;
    private int health;
    
    [SerializeField] private Transform rotate;
    [SerializeField] private GameObject gun;
    private bool isShooting;

    [SerializeField] private GameObject bullet;
    private float cooldown = 5f;
    private float timer = 0;
    

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        if (enemySO == null) return;
        if (enemySO.color != null)
        {
            sr.color = enemySO.color;
        }
        if (enemySO.health != 0)
        {
            health = enemySO.health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    public void HandleShooting()
    {
        if (timer > cooldown && isShooting)
        {
            Instantiate(bullet, transform.position, rotate.rotation);
            timer = 0;
        }
        
        
    }

    public void HandleAim(Transform targeted)
    {
        Vector3 rotation = targeted.position - rotate.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotate.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            isShooting = true;
            HandleAim(target);
            HandleShooting();
        }
        else
        {
            isShooting = false;
        }
    }
}
