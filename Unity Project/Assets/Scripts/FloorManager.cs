using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {

    public Sprite[] floorTemplates;
    private GameObject[] floors;

    void Start()
    {
        floors = GameObject.FindGameObjectsWithTag("floor");
        foreach (GameObject obj in floors)
        {
            int rand = Random.Range(0, floorTemplates.Length);
            obj.GetComponent<SpriteRenderer>().sprite = floorTemplates[rand];
        }

    }
}
