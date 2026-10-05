using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float speed;
    public Transform target;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
        ), speed * Time.deltaTime);
    }
}
