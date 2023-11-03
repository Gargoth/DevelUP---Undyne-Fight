using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float shieldHitShakeIntensity;
    [SerializeField] private float playerHitShakeIntensity;

    private GameManagerScript _gameManagerScript;
    private CameraScript _mainCamera;
    private AudioClip _shieldHitSound;
    private AudioClip _playerHurtSound;
    void Start()
    {
        _shieldHitSound = Resources.Load<AudioClip>("Audio/ArrowHit");
        _playerHurtSound = Resources.Load<AudioClip>("Audio/PlayerHurt");
        _gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();;
        _mainCamera = Camera.main.GetComponent<CameraScript>();
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
            _mainCamera.Shake(shieldHitShakeIntensity);
            _gameManagerScript.IncreaseScore();
            PlayShieldHitSound();
            CreateShieldHitParticles(other.GetContact(0).point);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Hit Player!");
            _mainCamera.Shake(playerHitShakeIntensity);
            _gameManagerScript.DecreaseHealth();
            CreatePlayerHurtParticles(other.GetContact(0).point);
            PlayPlayerHurtSound();
            Destroy(gameObject);
        }
    }

    private void CreateShieldHitParticles(Vector3 position)
    {
        GameObject particlePrefab = Resources.Load<GameObject>("Prefabs/Shield Particles");
        GameObject newObject = Instantiate(particlePrefab, position, quaternion.identity);
        Destroy(newObject, 1);
    }

    private void CreatePlayerHurtParticles(Vector3 position)
    {
        GameObject particlePrefab = Resources.Load<GameObject>("Prefabs/Player Hit Particles");
        GameObject newObject = Instantiate(particlePrefab, position, quaternion.identity);
        Destroy(newObject, 1);
    }

    private void PlayShieldHitSound()
    {
        GameObject newObject = Instantiate(new GameObject());
        Destroy(newObject, 0.5f);
        newObject.AddComponent<AudioSource>();
        AudioSource audio = newObject.GetComponent<AudioSource>();
        audio.PlayOneShot(_shieldHitSound, 0.5f);
    }

    private void PlayPlayerHurtSound()
    {
        GameObject newObject = Instantiate(new GameObject());
        Destroy(newObject, 0.5f);
        newObject.AddComponent<AudioSource>();
        AudioSource audio = newObject.GetComponent<AudioSource>();
        audio.PlayOneShot(_playerHurtSound, 0.75f);
    }
}
