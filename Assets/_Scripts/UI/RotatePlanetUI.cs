using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanetUI : MonoBehaviour
{
    public float Speed;
    private void Start()
    {
        RotateRandom = Random.Range(1f, 3f);
    }
    float RotateRandom;
    void Update()
    {
        n += Time.deltaTime * Speed * RotateRandom;
        transform.rotation = Quaternion.Euler(0, 0, n );
    }
    float n;
}
