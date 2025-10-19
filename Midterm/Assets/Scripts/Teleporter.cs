using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject teleporterTarget;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && teleporterTarget.activeSelf == true)
        {
            collision.transform.position = teleporterTarget.transform.position;
            StartCoroutine(TeleporterCooldown());
        }
    }

    private IEnumerator TeleporterCooldown()
    {
        teleporterTarget.SetActive(false);
        yield return new WaitForSeconds(2f);
        teleporterTarget.SetActive(true);
    }
}
