using UnityEngine;
using UnityEngine.Pool;

public class HealPickup : MonoBehaviour
{
    public float despawnTimer;
    private float lastDespawnTime;
    private IObjectPool<HealPickup> healPickupPool;

    // set the reference pool of the pickup
    public void SetPool(IObjectPool<HealPickup> pool)
    {
        healPickupPool = pool;
    }

    private void Update()
    {
        if (Time.time >= lastDespawnTime)
        {
            DespawnPickup();
            lastDespawnTime = Time.time + despawnTimer;
        }
        else
        {
            transform.Rotate(Vector3.up);
        }
    }

    // when despawning, this laser will release itself back to the pool
    public void DespawnPickup()
    {
        healPickupPool.Release(this);
    }
}
