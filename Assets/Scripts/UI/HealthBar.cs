using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBarSlider;
    
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = player.hp;
    }
}
