using System.Collections;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speed = 10f;
    [SerializeField] float delay = 1f;
    [SerializeField] GameObject platform;
    [SerializeField] bool onlyOnce = false;
    [SerializeField] bool startMoving = true;
    private bool alreadyMoving = false;

    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform.transform.position = pointA.transform.position;
        targetPos = pointB.transform.position;

        if (startMoving)
        {
            StartMoving();
        }
    }

    public void StartMoving()
    {
        if (alreadyMoving)
        {
            return;
        }

        alreadyMoving = true;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        bool running = true;
        while (running)
        {
            // while the platform has not reached the target pos
            while ((targetPos - platform.transform.position).sqrMagnitude > 0.01f)
            {
                // keep moving towards it
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }

            if (!onlyOnce)
            {
                // change target once a target is reached
                targetPos = targetPos == pointA.transform.position ? pointB.transform.position : pointA.transform.position;

                // delay movement first before going to next target
                yield return new WaitForSeconds(delay);
            }
            else
            {
                running = false;
            }
        }
    }
}
