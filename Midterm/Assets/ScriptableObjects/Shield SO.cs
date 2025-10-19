using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "PowerUps/Shield")]
public class ShieldSO : ScriptableObject
{
    [Header("Shield Settings")]
    public GameObject shieldPrefab;
}
