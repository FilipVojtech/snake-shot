using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Stats
    [SerializeField] private float speed = 9f;
    [SerializeField] private int health = 4;
    [SerializeField] private Shooting shooting;
    [SerializeField] private TextMeshProUGUI clawKillCounter, unityKillCounter, cSharpKillCounter;
    private int _clawKills = 0, _unityKills = 0, _cSharpKills = 0;

    // Movement
    private float _horizontalAxis, _verticalAxis;


    // Unity Methods
    private void Start()
    {
        _clawKills = 0;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (health <= 0) gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        MovementControl();
    }

    // Private Methods
    private void MovementControl()
    {
        // Získávání inputu
        _horizontalAxis = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        _verticalAxis = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;

        // Pohyb
        transform.position = new Vector3(transform.position.x + _horizontalAxis, transform.position.y + _verticalAxis);

        // Omezení pohybu
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.5f, 9.5f),
            Mathf.Clamp(transform.position.y, -9.5f, 9.5f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bone Pack"))
        {
            FindObjectOfType<Camera>().GetComponents<AudioSource>()[2].Play();
            shooting.AddAmmoStash(other.GetComponent<BonePickup>().ammoCount);
            Destroy(other.gameObject);
        }
    }

    // Public methods
    public void Damage()
    {
        health--;
    }

    public void AddKills(int enemyType)
    {
        switch (enemyType)
        {
            case 1:
                _clawKills++;
                break;
            case 2:
                _unityKills++;
                break;
            case 3:
                _cSharpKills++;
                if (health > 1)
                {
                    health++;
                }

                break;
        }

        clawKillCounter.text = "Claws killed: " + _clawKills;
        unityKillCounter.text = "Unity killed: " + _unityKills;
        cSharpKillCounter.text = "C# killed: " + _cSharpKills;
    }

    public int GetLives()
    {
        return health;
    }
}