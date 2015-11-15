using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float melee;
    public float magic;
    public float defense;
    public float health;

    [Header("Set particle system")]
    public ParticleSystem magicBall;

    private GameObject player;
    private Transform spawnPoint;
    private BoxCollider2D boxCollider;
    private float translateDuration = 0.5f;
    private float attackDuration = 0.5f;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        if (this.player == null)
            Debug.LogError("No player found");

        this.spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        if (this.spawnPoint == null)
            Debug.LogError("No spawnPoint found");

        this.boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        if (this.boxCollider == null)
            Debug.LogError("No boxCollider2D as component");

        this.gameObject.transform.position = this.spawnPoint.position;

        Invoke("Defense", 1);
    }

    public void Attack01()
    {
        Debug.Log("ATTACK 01");
        this.gameObject.transform.position = player.transform.position + new Vector3(0.0f, 1.0f);
        StartCoroutine(WaitToAttack01(attackDuration));
        

    }
    public void Attack02()
    {
        Debug.Log("ATTACK 02");
        StartCoroutine(Move(player.transform.position + new Vector3(0.0f, 1.0f)));
    }

    public void MagicAttack()
    {
        Debug.Log("MAGIC");
        ParticleSystem obj = (ParticleSystem)Instantiate(magicBall);
        obj.transform.position = this.transform.position;
        obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f,2.0f));
    }

    public void Defense()
    {
        Debug.Log("DEFENSE");
        this.boxCollider.enabled = false;
        StartCoroutine(WaitToDefense(1.0f));
    }

    IEnumerator WaitToAttack01(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.transform.position = this.spawnPoint.transform.position;
    }

    IEnumerator WaitToDefense(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.boxCollider.enabled = true;
    }

    private IEnumerator Move(Vector3 endP)
    {
        Vector3 startPosition = this.transform.position;
        Vector3 endPosition = endP;

        float counter = 0.0f;
        while (counter <= this.translateDuration)
        {
            counter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, counter / this.translateDuration);
            yield return null;
        }
        yield return new WaitForSeconds(attackDuration);
        StartCoroutine(Move(spawnPoint.transform.position));
        yield return null;
    }
}
