using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] public float speed;

    void Start()
    {
        Destroy(gameObject, 10f);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            // Debug.Log("Hit Shield!");
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Hit Player!");
            Destroy(gameObject);
        }
    }
}
