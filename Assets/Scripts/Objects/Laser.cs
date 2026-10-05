using UnityEngine;
using UnityEngine.Pool;

public class Laser : MonoBehaviour
{
    public float speed;
    public Transform target;

    private IObjectPool<Laser> laserPool;

    public void SetPool(IObjectPool<Laser> pool)
    {
        laserPool = pool;
    }

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
            DespawnLaser();
        }
    }

    public void DespawnLaser()
    {
        laserPool.Release(this);
    }
}
