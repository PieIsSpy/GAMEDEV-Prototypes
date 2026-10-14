using UnityEngine;

public class Target : MonoBehaviour
{
    private PlatformMovement movement;
    private Rigidbody rb;

    void Start()
    {
        movement = gameObject.GetComponentInParent<PlatformMovement>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            movement.StopMoving();
            rb.isKinematic = false;
            rb.WakeUp();

            Rigidbody projectileRb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(projectileRb.linearVelocity * projectileRb.mass, collision.contacts[0].point, ForceMode.Impulse);
        }
    }
}