using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private float timeSinceLastSpawn;

    [SerializeField] private Laser laserPrefab;
    [SerializeField] private float laserSpeed;
    private IObjectPool<Laser> laserPool;

    [SerializeField] private Transform target;
    public bool isActive = true;

    // create a pooler
    private void Awake()
    {
        laserPool = new ObjectPool<Laser>(CreateLaser, OnGet, OnRelease);
    }

    // when a laser is get, set to active and randomize
    private void OnGet(Laser laser)
    {
        laser.gameObject.SetActive(true);
        RandomizeLaser(laser);
    }

    // when released, set to not active
    private void OnRelease(Laser laser)
    {
        laser.gameObject.SetActive(false);
    }

    // set the defaults of lasers based on spawner fields
    private Laser CreateLaser()
    {
        Laser laser = Instantiate(laserPrefab);
        laser.SetPool(laserPool);
        RandomizeLaser(laser);
        laser.target = target;
        laser.speed = laserSpeed;
        return laser;
    }

    // get one laser from pool per interval
    void Update()
    {
        if (isActive && Time.time >= timeSinceLastSpawn)
        {
            laserPool.Get();
            timeSinceLastSpawn = Time.time + spawnInterval;
        }
    }

    void RandomizeLaser(Laser laser)
    {
        laser.transform.position = RandomPosition();
        laser.transform.Rotate(RandomRotation());
    }

    Vector3 RandomPosition()
    {
        float height = gameObject.GetComponent<BoxCollider>().size.y;
        float y = Random.Range(2, height);
        return new Vector3(
            gameObject.transform.position.x,
            y,
            gameObject.transform.position.z
        );
    }

    Vector3 RandomRotation()
    {
        float z = Random.Range(60, 120);
        return new Vector3(0, 0, z);
    }
}
