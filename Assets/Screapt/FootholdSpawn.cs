using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootholdSpawn : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int startNumFoothold = 8;
    [SerializeField] float xRange = 4;
    [SerializeField] float zLength = 5;
    private int platformIndex = 0;
    public float ZLength
    {
        get { return zLength; }
    }

    private void Awake()
    {
        for (int i = 0; i < startNumFoothold; i++)
        {
            SpawnFoothold();
        }
    }

    public void SpawnFoothold()
    {
        GameObject clone = Instantiate(platformPrefab);
        clone.GetComponent<NewFoothold>().Setup(this);
        ResetFoothold(clone.transform);
    }

    public void ResetFoothold(Transform transform, float y = 0)
    {
        platformIndex++;
        float x = Random.Range(-xRange, xRange);
        transform.position = new Vector3(x, 0, platformIndex * zLength);
    }
}