using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewFoothold : MonoBehaviour
{
    private Camera arCamera;
    private FootholdSpawn platformSpawner;
    private float yMoveTime = 0.5f;

    public void Setup(FootholdSpawn platformSpawner)
    {
        this.platformSpawner = platformSpawner;
        arCamera = Camera.main;
    }

    private void Update()
    {
        if (arCamera.transform.position.z - transform.position.z > 0)
        {
            platformSpawner.ResetFoothold(this.transform);
        }
    }
}