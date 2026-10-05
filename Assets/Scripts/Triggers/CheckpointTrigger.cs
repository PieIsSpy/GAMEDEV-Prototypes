using System;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.gameObject.TryGetComponent(out Player player);
            player.currentCheckpoint = transform;
            print("Checkpoint!");
        }
    }
}
