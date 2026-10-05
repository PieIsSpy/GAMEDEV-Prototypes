using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int hp = 100;
    private int base_hp;
    [SerializeField] public int speedMultiplier = 1;
    [SerializeField] public int jumpMultiplier = 1;
    [SerializeField] public float threshold;
    [SerializeField] public Transform currentCheckpoint;
    private Transform body;

    private void Start()
    {
        base_hp = hp;
        body = transform.Find("PlayerArmature");
    }

    private void FixedUpdate()
    {
        bool fellOver = body.transform.position.y < threshold;
        bool died = hp <= 0;
        if (currentCheckpoint && (fellOver || died)) {
            Respawn();
        }
    }

    public void Respawn()
    {
        print("You Died!");
        ResetInfo();
        ReturnToCheckpoint();
    }

    public void ResetInfo()
    {
        hp = base_hp;
        speedMultiplier = 1;
        jumpMultiplier = 1;
    }

    public void ReturnToCheckpoint()
    {
        body.transform.position = new Vector3(
            currentCheckpoint.position.x, 
            currentCheckpoint.position.y + 1,
            currentCheckpoint.position.z
        );
    }
}
