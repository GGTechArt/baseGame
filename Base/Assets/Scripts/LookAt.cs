using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void Start()
    {
        transform.forward = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z) - transform.position;
    }
}
