using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {
    [Header("Set floor Sprites")]
    public Sprite[] floorTemplates;
    private GameObject[] floors;

    private void Start()
    {
        this.floors = GameObject.FindGameObjectsWithTag("floor");
        if (this.floors == null)
        {
            Debug.LogError("Any object has the tag");
            this.enabled = false; 
        }
        foreach (GameObject obj in floors)
        {
            int rand = Random.Range(0, this.floorTemplates.Length);
            obj.GetComponent<SpriteRenderer>().sprite = this.floorTemplates[rand];
        }

    }
}
