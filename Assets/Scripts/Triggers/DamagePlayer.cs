using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private Laser laser;
    void Start()
    {
        TryGetComponent(out laser);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.gameObject.TryGetComponent(out Player player);
            player.hp -= 10;
            print("Ouch!");
            laser.DespawnLaser();
        }
    }
}
