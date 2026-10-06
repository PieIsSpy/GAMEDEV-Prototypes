using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endUI;
    [SerializeField] private LaserSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        endUI.enabled = true;
        spawner.isActive = false;
    }
}
