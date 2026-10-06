using UnityEngine;
using UnityEngine.Pool;

public class HealPickupSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private float timeSinceLastSpawn;

    [SerializeField] private HealPickup pickupPrefab;
    [SerializeField] private float despawnTimer;
    private IObjectPool<HealPickup> pickupPool;

    public bool isActive = true;

    private void Awake()
    {
        pickupPool = new ObjectPool<HealPickup>(CreatePickup, OnGet, OnRelease);
    }

    private void OnGet(HealPickup pickup)
    {
        pickup.gameObject.SetActive(true);
        RandomizePickup(pickup);
    }

    private void OnRelease(HealPickup pickup)
    {
        pickup.gameObject.SetActive(false);
    }

    // setup pickup instance for pool
    private HealPickup CreatePickup()
    {
        HealPickup pickup = Instantiate(pickupPrefab);
        pickup.SetPool(pickupPool);
        RandomizePickup(pickup);
        pickup.despawnTimer = despawnTimer;
        return pickup;
    }

    void Update()
    {
        if (isActive && Time.time >= timeSinceLastSpawn)
        {
            pickupPool.Get();
            timeSinceLastSpawn = Time.time + spawnInterval;
        }
    }

    void RandomizePickup(HealPickup pickup)
    {
        Vector3 playerLocation = transform.position;
        float xRadius = 30f;
        float zRadius = 5f;

        // randomly choose an offset
        float xOffset = Random.Range(-xRadius, -3);
        float zOffset = Random.Range(-zRadius, zRadius);

        // add the offset vector to the player location
        Vector3 newSpawn = playerLocation + new Vector3(xOffset, 1, zOffset);
        pickup.transform.position = newSpawn;
    }
}
