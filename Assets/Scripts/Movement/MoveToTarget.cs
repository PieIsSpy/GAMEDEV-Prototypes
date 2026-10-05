using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float speed;
    public Transform target;

    private void Update()
    {
        Vector3 targetPos = new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
        );

        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
