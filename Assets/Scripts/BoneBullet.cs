using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoneBullet : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        FindObjectOfType<Camera>().GetComponents<AudioSource>()[1].Play();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + Random.Range(3f, 10f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 3f);
    }
}