using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public float floatSpeed = 0.5f;
    public float floatAmplitude = 0.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}