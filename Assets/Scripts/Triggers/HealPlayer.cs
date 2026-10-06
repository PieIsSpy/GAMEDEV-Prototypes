using System;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    private HealPickup pickup;
    void Start()
    {
        TryGetComponent(out pickup);
    }

    // when the pickup hits the player armature, heal the player and release the pickup back to pool
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.gameObject.TryGetComponent(out Player player);
            player.hp = Math.Min(player.hp + 10, player.base_hp);
            pickup.DespawnPickup();
        }
    }
}
