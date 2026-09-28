using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float threshold;
    [SerializeField] public Transform currentCheckpoint;

    private void FixedUpdate()
    {
        if (currentCheckpoint && transform.position.y < threshold) {
            transform.position = currentCheckpoint.position + (Vector3.up * 2);
        }
    }
}
