using UnityEngine;
using System.Collections;

public class Cleaner : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
