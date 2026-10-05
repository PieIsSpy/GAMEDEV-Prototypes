using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int defaultHP = 100;
    [SerializeField] int speedMultiplier = 1;
    [SerializeField] int jumpMultiplier = 1;
    [SerializeField] private float threshold;
    [SerializeField] Transform currentCheckpoint;
    private Transform body;

    private void Start()
    {
        body = transform.Find("PlayerArmature");
    }

    private void FixedUpdate()
    {
        if (currentCheckpoint && body.transform.position.y < threshold) {
            Respawn();
        }
    }

    private void Respawn()
    {
        body.transform.position = new Vector3(
            currentCheckpoint.position.x, 
            currentCheckpoint.position.y + 1,
            currentCheckpoint.position.z
        );
    }
}
