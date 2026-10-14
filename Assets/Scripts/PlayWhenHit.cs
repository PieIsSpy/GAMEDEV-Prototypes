using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayWhenHit : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] hitClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            randomizeSound();
        }
    }

    private void randomizeSound()
    {
        int idx = Random.Range(0, hitClips.Length);
        AudioClip clip = hitClips[idx];
        audioSource.PlayOneShot(clip);
    }
}
