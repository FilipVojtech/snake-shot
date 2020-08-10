using System;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private int
        health = 2,
        ammoForKilling = 2;

    [SerializeField] private float speed = 1;
    [SerializeField] private Rigidbody2D thisRigidbody;
    [SerializeField] private GameObject bonePack;
    [SerializeField] private BonePickup bonePickup;


    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.GetComponent<Player>().Damage();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet 1"))
        {
            Debug.Log("Got hit");
            health--;
            Destroy(other.gameObject);

            if (health <= 0)
            {
                _player.GetComponent<Player>().AddKills(2);
                GameObject ammoDrop = Instantiate(bonePack, transform.position, Quaternion.identity);
                ammoDrop.GetComponent<BonePickup>().ammoCount = ammoForKilling;
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        // transform.LookAt(_player.transform);
        transform.position =
            Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
    }
}