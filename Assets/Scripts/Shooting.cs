using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private int ammoCount = 10, ammoCapacity = 20;
    [SerializeField] private float bulletForce = 20f, cooldown = 0.3f;
    private float _cooldownTime;
    [SerializeField] private GameObject bulletPrefab, bonePackPrefab;
    [SerializeField] private Transform shootPoint;
    private Vector2 _mousePos;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D thisRigidbody;
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private ScreensController screensController;

    private void Update()
    {
        Vector2 lookDirection = _mousePos - thisRigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        thisRigidbody.rotation = angle;

        if (Input.GetButtonDown("Fire1") && _cooldownTime < Time.time && ammoCount > 0 &&
            !screensController.GetIsPaused())
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
            _cooldownTime = Time.time + cooldown;
            ammoCount--;
            Destroy(bullet, 3f);
        }

        _mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        ammoCountText.text = ammoCount.ToString();

        if (ammoCount < 1 && GameObject.FindGameObjectsWithTag("Bone Pack").Length < 1 &&
            GameObject.FindGameObjectsWithTag("Bullet 1").Length < 1)
        {
            GameObject ammoDrop = Instantiate(bonePackPrefab, new Vector2(0, 0), Quaternion.identity);
            ammoDrop.GetComponent<BonePickup>().ammoCount = 4;
        }
    }

    public void AddAmmoStash(int ammoCount)
    {
        if (this.ammoCount + ammoCount > ammoCapacity) this.ammoCount = ammoCapacity;
        else this.ammoCount += ammoCount;
    }
}