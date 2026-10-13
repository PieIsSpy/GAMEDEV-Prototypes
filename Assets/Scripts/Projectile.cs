using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private IObjectPool<Projectile> projectilePool;
    public float despawnTimer = 10f;
    private bool toDespawn;
    private Coroutine despawnCoroutine;

    public void SetPool(IObjectPool<Projectile> pool)
    {
        projectilePool = pool;
    }

    private void OnEnable()
    {
        toDespawn = false;
        despawnCoroutine = StartCoroutine(DespawnAfterDelay());
    }

    private void OnDisable()
    {
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
        }
    }

    private IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(despawnTimer);
        Despawn();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            return;
        }

        Despawn();
    }

    public void Despawn()
    {
        if (!toDespawn)
        {
            toDespawn = true;
            projectilePool.Release(this);
        }
    }
}
