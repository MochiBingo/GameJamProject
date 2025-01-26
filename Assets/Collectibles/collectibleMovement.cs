using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleMovement : MonoBehaviour
{
    [Header("Floating Motion")]
    [SerializeField] private float floatAmplitude = 0.5f; // How far up and down it moves
    [SerializeField] private float floatFrequency = 1f;   // How fast it moves up and down
    
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;   // How fast it spins (degrees per second)
    
    private Vector3 startPosition;

    private void Start()
    {
        // Store the initial position
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the floating motion using a sine wave
        float newY = startPosition.y + (floatAmplitude * Mathf.Sin(Time.time * floatFrequency));
        
        // Update the position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        // Rotate around the Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}