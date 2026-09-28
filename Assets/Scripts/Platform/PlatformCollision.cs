using System;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform platform;
    [SerializeField] PlatformMovement movingPlatform;

    private void OnTriggerEnter(Collider other)
    {
        // if the player is on the platform, then parent player to the platform
        // and trigger StartMoving() if movement is trigger based
        if (other.gameObject.CompareTag(playerTag))
        {
            other.gameObject.transform.parent = platform;
            movingPlatform.StartMoving();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
