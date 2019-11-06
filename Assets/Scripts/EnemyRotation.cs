using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField]
    [Range(0,100)]
    private float rotationSpeed;
    [SerializeField]
    private Rigidbody bodyRig;
    [SerializeField]
    private Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        bodyRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPoint;
        bodyRig.angularVelocity = new Vector3(0, rotationSpeed, 0);
        bodyRig.velocity = Vector3.zero;
    }
}
