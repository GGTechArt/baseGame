using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterConfig>())
        {
            Destroy(other.gameObject);
        }
    }
}
