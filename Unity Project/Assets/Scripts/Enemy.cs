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
    private float maxHealth;
    private float maxMagic;

    [Header("Set particle system")]
    public ParticleSystem magicBall;

    private GameObject player;
    private PlayerController playerC;
    private Transform spawnPoint;
    private BoxCollider2D boxCollider;
    private float translateDuration = 0.5f;
    private float attackDuration = 0.5f;

    public GameObject healthBar;
    public GameObject magicBar;
    private float HBLength;
    private bool canIDefend = false;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        if (this.player == null)
            Debug.LogError("No player found");

        this.playerC = player.GetComponent<PlayerController>();

        this.spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        if (this.spawnPoint == null)
            Debug.LogError("No spawnPoint found");

        this.boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        if (this.boxCollider == null)
            Debug.LogError("No boxCollider2D as component");

        this.gameObject.transform.position = this.spawnPoint.position;
        this.maxHealth = this.health;
        this.maxMagic = this.magic;
        this.magic = 0;
        this.HBLength = this.healthBar.transform.localScale.x;
        StartCoroutine(AddjustCurrentMana(+1));

        Invoke("Attack01", 2);
        Invoke("Defense", 5);
    }
    void Update()
    {
        if (canIDefend == true)
        {
            Debug.Log("Charged");
            canIDefend = false;
            magic = 0;
            int rnd = Random.Range(0, 4);
            Invoke("MagicAttack", rnd);
            StartCoroutine(AddjustCurrentMana(+1));

        }

    }
    public void Attack01()
    {
        Debug.Log("ATTACK 01");
        this.gameObject.transform.position = player.transform.position + new Vector3(0.0f, 1.0f);
        StartCoroutine(WaitToAttack01(attackDuration));
        Invoke("Attack01", 2);
        playerC.AddjustCurrentHealth((int)-melee);


    }
    public void Attack02()
    {
        Debug.Log("ATTACK 02");
        StartCoroutine(Move(player.transform.position + new Vector3(0.0f, 1.0f)));
        playerC.AddjustCurrentHealth((int)-melee);
    }

    public void MagicAttack()
    {
        Debug.Log("MAGIC");
        ParticleSystem obj = (ParticleSystem)Instantiate(magicBall);
        obj.transform.position = this.transform.position;
        obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f,2.0f));
        playerC.AddjustCurrentHealth((int)-magic/3);
    }

    public void Defense()
    {
        int rnd = Random.Range(0, 101);
        if (rnd < 75)
        {
            Debug.Log("DEFENSE");
            this.boxCollider.enabled = false;
            StartCoroutine(WaitToDefense(1.0f));
        }
        Invoke("Defense", 5);

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

    public void AddjustCurrentHealth(int adj)
    {

        this.health += adj;

        if (this.health < 0)
            Destroy(this.gameObject);

        if (this.health > this.maxHealth)
            this.health = this.maxHealth;

        if (this.maxHealth < 1)
            Destroy(this.gameObject);

        this.healthBar.transform.localScale = new Vector3(this.HBLength * (this.health / (float)maxHealth), this.HBLength, this.HBLength);
    }

    private IEnumerator AddjustCurrentMana(int adj)
    {
        while (this.magic < this.maxMagic)
        {
            Debug.Log("I'm recharging magic"); 
            this.magic += 0.5f;
            yield return null;
        }
        Debug.Log("END");
        canIDefend = true;
        yield return null;
    }
}
