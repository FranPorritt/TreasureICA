using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    private Quaternion startRotation;
    // Use this for initialization
    void Start()
    {
        startRotation.Set(20, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = startRotation;
    }
}
