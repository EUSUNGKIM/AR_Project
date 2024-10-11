using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 position = transform.position;
        position.z = target.position.z - 10;
        transform.position = position;
    }
}