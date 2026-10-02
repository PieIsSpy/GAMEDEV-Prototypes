using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float threshold;
    [SerializeField] public Transform currentCheckpoint;

    private void FixedUpdate()
    {
        if (currentCheckpoint && transform.position.y < threshold) {
            transform.position = new Vector3(
                currentCheckpoint.position.x, 
                currentCheckpoint.position.y + 7,
                currentCheckpoint.position.z);
        }
    }
}
