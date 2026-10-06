using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public int base_hp = 100;
    public float threshold;
    public Transform currentCheckpoint;
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
        ResetInfo();
        ReturnToCheckpoint();
    }

    public void ResetInfo()
    {
        hp = base_hp;
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
