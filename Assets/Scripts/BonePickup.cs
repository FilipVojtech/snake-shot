using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonePickup : MonoBehaviour
{
    public int ammoCount = 2;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(0, transform.rotation.y + 1);
    }
}