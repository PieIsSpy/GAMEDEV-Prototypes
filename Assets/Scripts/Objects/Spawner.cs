using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private float timeSinceLastSpawn;

    [SerializeField] private Laser laserPrefab;
    private IObjectPool<Laser> laserPool;

    [SerializeField] private Transform target;

    private void Awake()
    {
        laserPool = new ObjectPool<Laser>(CreateLaser, OnGet, OnRelease);
    }

    private void OnGet(Laser laser)
    {
        laser.gameObject.SetActive(true);
        laser.transform.position = gameObject.transform.position;
    }

    private void OnRelease(Laser laser)
    {
        laser.gameObject.SetActive(false);
    }

    private Laser CreateLaser()
    {
        Laser laser = Instantiate(laserPrefab);
        laser.SetPool(laserPool);
        laser.transform.position = gameObject.transform.position;
        laser.target = target;
        return laser;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeSinceLastSpawn)
        {
            laserPool.Get();
            timeSinceLastSpawn = Time.time + spawnInterval;
        }
    }
}
