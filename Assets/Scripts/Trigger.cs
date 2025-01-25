using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject Text;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Text.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
    }
}
