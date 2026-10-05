using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int HP = 100;
    [SerializeField] public int speedMultiplier = 1;
    [SerializeField] public int jumpMultiplier = 1;
    [SerializeField] public float threshold;
    [SerializeField] public Transform startingPosition;
    public Transform currentCheckpoint;
    private Transform body;

    private void Start()
    {
        body = transform.Find("PlayerArmature");
        currentCheckpoint = startingPosition;
    }

    private void FixedUpdate()
    {
        if (startingPosition && body.transform.position.y < threshold) {
            Respawn();
        }
    }

    public void Respawn()
    {
        body.transform.position = new Vector3(
            currentCheckpoint.position.x, 
            currentCheckpoint.position.y + 1,
            currentCheckpoint.position.z
        );
    }

    public void ResetInfo()
    {
        speedMultiplier = 1;
        jumpMultiplier = 1;
    }
}
