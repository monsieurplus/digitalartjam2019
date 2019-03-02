using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, Time.time * rotationSpeed);
    }
}
