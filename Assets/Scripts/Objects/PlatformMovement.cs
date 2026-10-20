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
    public bool isRunning = true;
    private bool alreadyMoving = false;
    private Coroutine moveCoroutine;


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
        isRunning = true;
        moveCoroutine = StartCoroutine(MovePlatform());
    }

    public void StopMoving()
    {
        isRunning = false;
        alreadyMoving = false;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    IEnumerator MovePlatform()
    {
        while (isRunning)
        {
            // while the platform has not reached the target pos
            while (((targetPos - platform.transform.position).sqrMagnitude > 0.01f) && isRunning)
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
                float timer = 0f;
                while (timer < delay && isRunning)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                isRunning = false;
            }
        }
    }
}
