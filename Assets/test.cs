using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody rb;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 patrolPoint;
    float patrolRange = 2f;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = GameObject.Find("Player").transform.position;
        startPosition = transform.position;
        patrolPoint = new Vector3(Random.Range(startPosition.x - patrolRange, startPosition.x + patrolRange), 0f, Random.Range(startPosition.z - patrolRange, startPosition.z + patrolRange));
        Debug.Log(patrolPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit, 1000))
        //    {
        //        targetPosition = hit.point;
        //        targetPosition.y = 0;
        //        transform.LookAt(targetPosition);
        //        rb.MovePosition(targetPosition);
        //    }
        //}     

    }

    private void FixedUpdate()
    {

    }
}
