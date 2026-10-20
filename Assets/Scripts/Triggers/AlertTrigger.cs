using UnityEngine;

[RequireComponent(typeof(Light))]
public class AlertTrigger : MonoBehaviour
{
    public LayerMask obstacleMask;
    public float targetHeightOffset = 1.0f;
    [SerializeField] private bool setOffOnce = true;

    private Light spotLight;
    private Transform playerTransform;
    private bool isActive = true;

    private void Awake()
    {
        spotLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        if (IsPlayerInSpotlight() && isActive)
        {
            if (setOffOnce)
            {
                isActive = false;
            }
            Debug.Log("Player detected in Spotlight!");
        }
    }

    public bool IsPlayerInSpotlight()
    {
        if (spotLight == null || playerTransform == null)
        {
            return false;
        }

        // target center of player
        Vector3 targetPosition = playerTransform.position + (Vector3.up * targetHeightOffset);
        Vector3 directionToPlayer = targetPosition - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // check distance range if it reaches the player
        if (distanceToPlayer > spotLight.range)
        {
            return false;
        }

        // check cone angle if it hits the player
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > spotLight.spotAngle / 2f)
        {
            return false;
        }

        // check if any obstacle layer was hit first before the player
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, distanceToPlayer, obstacleMask))
        {
            return false;
        }

        return true;
    }
}
