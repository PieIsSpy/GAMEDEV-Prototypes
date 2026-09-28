using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform checkpoint;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.CompareTag(playerTag))
        {
            other.TryGetComponent<PlayerRespawn>(out PlayerRespawn playerRespawn);
            playerRespawn.currentCheckpoint = checkpoint.transform;
            isTriggered = true;
        }
    }
}
