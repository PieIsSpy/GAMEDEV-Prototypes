using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int hp = 100;
    private int base_hp;
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

            if (fellOver)
            {
                print("Player escaped the Matrix.");
            }
            else if (died)
            {
                print("Player was sent to Israel by Laser.");
            }
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
