using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int defaultHP = 100;
    [SerializeField] public int speedMultiplier = 1;
    [SerializeField] public int jumpMultiplier = 1;
    [SerializeField] public float threshold;
    [SerializeField] public Transform currentCheckpoint;
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
