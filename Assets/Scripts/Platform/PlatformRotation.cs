using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;
    [SerializeField] float delay;
    private Coroutine rotateCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rotateCoroutine = StartCoroutine(RotatePlatform());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
        }
    }

    IEnumerator RotatePlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            platform.Rotate(new Vector3(xRotation, yRotation, zRotation));
        }
    }
}
