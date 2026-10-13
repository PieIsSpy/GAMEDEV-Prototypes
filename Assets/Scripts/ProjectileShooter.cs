using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileShooter : MonoBehaviour
{
    public Projectile bullet;

    public float shootForce, upwardForce;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, projectilesPerTap;
    public bool allowButtonHold;

    int projectilesLeft, projectilesShot;
    bool shooting, readyToShoot, reloading;
    bool allowInvoke = true;

    public Camera fpsCam;
    public Transform attackPoint;

    private IObjectPool<Projectile> projectilesPool;

    public void Awake()
    {
        projectilesPool = new ObjectPool<Projectile>(CreateProjectile, OnGet, OnRelease);
        projectilesLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void OnGet(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        ProjectileMotion(projectile);
    }

    private void OnRelease(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void MyInput()
    {
        // check if full auto
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // reloading
        if (Input.GetKeyDown(KeyCode.R) && projectilesLeft < magazineSize && !reloading)
            Reload();

        // shooting
        if (readyToShoot && shooting && !reloading && projectilesLeft > 0)
        {
            projectilesShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // spawn bullet
        projectilesPool.Get();

        projectilesLeft--;
        projectilesShot++;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
            allowInvoke = false;
        }

        if (projectilesShot < projectilesPerTap && projectilesLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        projectile.SetPool(projectilesPool);
        return projectile;
    }

    private void ProjectileMotion(Projectile projectile)
    {
        // set target to middle of the screen
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // raycast towards a surface
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        // find the projectile direction
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // randomize spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        // add spread if applicable
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        // clear rigid body velocity
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // move projectile to gun muzzle
        Quaternion targetRotation = Quaternion.LookRotation(directionWithSpread.normalized);
        projectile.transform.SetPositionAndRotation(attackPoint.position, targetRotation);

        // apply force
        rb.AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke(nameof(RelaodingFinished), timeBetweenShots);
    }

    private void RelaodingFinished()
    {
        projectilesLeft = magazineSize;
        reloading = false;
    }
}
