using UnityEngine;
using System.Collections;

public class EnemySpawnPoint : MonoBehaviour {

    [Header("Enemy Pooling")]
    public GameObject[] enemyTypePrefab;

    void Start () {
        int rnd = Random.Range(0, enemyTypePrefab.Length);
        GameObject obj = (GameObject)Instantiate(enemyTypePrefab[rnd]);
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        obj.transform.position = spawnPoint.transform.position;
        Destroy(this.gameObject);
    }

}
