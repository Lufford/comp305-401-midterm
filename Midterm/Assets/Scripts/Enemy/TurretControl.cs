using System;
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
    private float cooldown = 3f;
    private float timer = 0;

    public int scoreValue = 1;
    

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
        if(target != null)
        { 
            timer += Time.deltaTime;
        }
        if (health == 0)
        {

            GameManager.Instance.updateScore(scoreValue);
            Destroy(gameObject);
        }
    }

    public void HandleShooting()
    {
        if (timer > cooldown && isShooting)
        {
            Instantiate(bullet, gun.transform.position, rotate.rotation);
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health--;
            Debug.Log("Turret Health: " + health);
        }
    }
}
