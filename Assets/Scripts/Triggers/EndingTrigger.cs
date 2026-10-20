using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] GameState state;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            state.GameEnd();
        }
    }
}
