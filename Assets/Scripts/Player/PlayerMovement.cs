using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // get player inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // get the direction of the inputs
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // move the player
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}
