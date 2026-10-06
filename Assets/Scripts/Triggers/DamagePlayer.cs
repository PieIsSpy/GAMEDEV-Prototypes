using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private Laser laser;
    void Start()
    {
        TryGetComponent(out laser);
    }

    // when the laser hits the player armature, damage the player and release the laser back to pool
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.gameObject.TryGetComponent(out Player player);
            player.hp -= 10;
            laser.DespawnLaser();
        }
    }
}
