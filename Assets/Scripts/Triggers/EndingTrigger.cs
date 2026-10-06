using TMPro;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endUI;
    [SerializeField] private LaserSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            endUI.enabled = true;
            spawner.isActive = false;
            other.gameObject.TryGetComponent(out HealPickupSpawner healer);
            healer.isActive = false;
        }
    }
}
