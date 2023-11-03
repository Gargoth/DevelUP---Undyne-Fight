using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float testShakeIntensity;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private bool testShake;
    
    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed);
        
        // For debug purposes
        if (testShake)
        {
            testShake = false;
            Shake(testShakeIntensity);
        }
    }

    public void Shake(float intensity)
    {
        float xOffset = Random.Range(0, intensity);
        float yOffset = Random.Range(0, intensity);
        float zOffset = Random.Range(0, intensity);
        transform.position += new Vector3(xOffset, yOffset, zOffset);
    }
}
