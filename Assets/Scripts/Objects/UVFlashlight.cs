using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class UVFlashlight : MonoBehaviour
{
    public float range = 10f;
    public LayerMask uvLayer;

    public Material hiddenMaterial;
    public Material revealedMaterial;

    private Renderer lastHitRenderer;
    private Light lightComponent;
    private bool isLightOn = false;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        lightComponent.enabled = isLightOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLightOn = !isLightOn;
            lightComponent.enabled = isLightOn;
        }

        if (isLightOn && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, uvLayer))
        {
            if (hit.collider.TryGetComponent<Renderer>(out var rend))
            {
                if (lastHitRenderer != null && lastHitRenderer != rend)
                {
                    lastHitRenderer.material = hiddenMaterial;
                }

                rend.material = revealedMaterial;
                lastHitRenderer = rend;
                return;
            }
        }

        if (lastHitRenderer != null)
        {
            lastHitRenderer.material = hiddenMaterial;
            lastHitRenderer = null;
        }
    }
}