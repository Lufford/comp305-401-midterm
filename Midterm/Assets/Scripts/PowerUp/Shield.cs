using UnityEngine;

public class Shield : MonoBehaviour
{
    public ShieldSO shieldData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShieldMechanics shield = other.GetComponent<ShieldMechanics>();

            if (shield == null)
                shield = other.gameObject.AddComponent<ShieldMechanics>();

            shield.ShieldActive(shieldData.shieldPrefab);
            Destroy(gameObject);
        }
    }

}
