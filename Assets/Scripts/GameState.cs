using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endUI;
    [SerializeField] private LaserSpawner spawner;
    [SerializeField] private HealPickupSpawner healer;
    private Light[] spotlights;

    private void Start()
    {
        GameObject[] spotlightObjects = GameObject.FindGameObjectsWithTag("Spotlight");
        spotlights = new Light[spotlightObjects.Length];

        for (int i = 0; i < spotlightObjects.Length; i++)
        {
            if (spotlightObjects[i].TryGetComponent<Light>(out var lightComp))
            {
                spotlights[i] = lightComp;
            }
        }
    }

    public void GameEnd()
    {
        endUI.enabled = true;
        spawner.isActive = false;
        healer.isActive = false;
        changeSpotlightColors(Color.green);
    }

    public void GameAlerted()
    {
        spawner.isActive = true;
        changeSpotlightColors(Color.red);
    }

    private void changeSpotlightColors(Color color)
    {
        foreach (Light spotlight in spotlights)
        {
            spotlight.color = color;
        }
    }
}
