using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthSystem healthSystem;
    private float horizontalMovement;
    private float verticalMovement;
    public float speed = 5f;
    public float rotationSpeed = 1f;
    private Rigidbody rb;
    private Entity enemy;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Awake()
    {
        healthSystem = new HealthSystem(100);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        velocity = transform.forward * verticalMovement * speed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Rotation()
    {
        if (horizontalMovement != 0)
        {
            transform.Rotate(0f, horizontalMovement * rotationSpeed * Time.fixedDeltaTime, 0f);
        }
        else return;
    }
}
