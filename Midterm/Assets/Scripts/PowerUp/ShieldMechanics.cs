using UnityEngine;

public class ShieldMechanics : MonoBehaviour
{
    private GameObject activeShield;
    private bool shieldActive = false;
    public bool IsShieldActive => shieldActive;

    public void ShieldActive(GameObject shieldPrefab)
    {
        if (shieldActive) return;

        activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        shieldActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shieldActive && collision.CompareTag("EnemyBullet"))
        {
            Destroy(activeShield);
            shieldActive = false;
            Destroy(this);
            Destroy(collision.gameObject);
        }
    }
}
