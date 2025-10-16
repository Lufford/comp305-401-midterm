using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Start()
    {
        Instantiate(bullet, this.transform.position, this.transform.rotation);
        Destroy(gameObject, 0.5f);
    }
}
