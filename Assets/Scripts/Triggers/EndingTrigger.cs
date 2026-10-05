using TMPro;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endUI;

    private void OnTriggerEnter(Collider other)
    {
        endUI.enabled = true;
    }
}
