using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Set particle system")]
    public ParticleSystem magicBall;
    //Definir si al dar un golpe saldran otras particulas

    //Player EXP
    private int EXP;
    private int Faction;
    private StatsManager stats;
    public float magic;
    public float melee;
    public float defense;
    public float health;

    private GameObject enemy;
    private Enemy enemyEn;
    private BoxCollider2D boxColl;
    private Vector3 spawnPoint;
    private float translateDuration = 0.5f;
    private float attackDuration = 0.5f;
    private GameObject shield;

    public GameObject healthBar;
    public GameObject magicBar;
    private float HBLength;
    private float maxHealth;
    private float maxMagic;

    // Use this for initialization
    void Start()
    {
        this.enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (this.enemy == null)
            Debug.LogError("No enemy found");

        this.enemyEn = enemy.GetComponent<Enemy>();

        this.boxColl = this.gameObject.GetComponent<BoxCollider2D>();
        if (this.boxColl == null)
            Debug.LogError("No boxCollider2D as component");

        this.shield = this.transform.GetChild(0).gameObject;
        if (this.shield == null)
            Debug.LogError("No shield as children");
        else
            shield.SetActive(false);

        this.spawnPoint = this.transform.position;

        this.maxHealth = this.health;
        this.maxMagic = this.magic;
        this.magic = 0;
        this.HBLength = this.healthBar.transform.localScale.x;

        if (PlayerPrefs.HasKey("PLAYER_EXP"))
        {
            this.EXP = PlayerPrefs.GetInt("PLAYER_EXP");
        }
        else
        {
            this.EXP = 100;
            PlayerPrefs.SetInt("PLAYER_EXP", EXP);
        }
        Faction = PlayerPrefs.GetInt("FACTION");
        stats = GetComponent<StatsManager>();
        StartCoroutine(AddjustCurrentMana(+1));
    }

    void Update()
    {
    }

    void OnEnable()
    {
        //subscribe to an event
        IT_Gesture.onMultiTapE += Attack02;
        IT_Gesture.onSwipeEndE += Defense;
    }
    void OnDisable()
    {
        //unsubscribe to an event
        IT_Gesture.onMultiTapE -= Attack02;
        IT_Gesture.onSwipeEndE -= Defense;
    }

    public void Attack01(Tap tap)
    {

        Debug.Log("ATTACK 01");
        this.gameObject.transform.position = this.enemy.transform.position + new Vector3(0.0f, -1.0f);
        StopAllCoroutines();
        StartCoroutine(WaitToAttack01(attackDuration));
        Debug.Log("enemy health: " + enemyEn.health);
        enemyEn.AddjustCurrentHealth((int)-melee);


    }
    public void Attack02(Tap tap)
    {
        Debug.Log("ATTACK 02");
        StopAllCoroutines();
        StartCoroutine(Move(enemy.transform.position + new Vector3(0.0f, -1.0f)));
        enemyEn.AddjustCurrentHealth((int)-melee);
    }
    public void Defense(SwipeInfo swipe)
    {
        Debug.Log("DEFENSE");
        shield.SetActive(true);
        this.boxColl.enabled = false;
        StopAllCoroutines();
        StartCoroutine(WaitToDefense(1.0f));
    }
    public void MagicAttack()
    {
        Debug.Log("MAGIC");
        ParticleSystem obj = (ParticleSystem)Instantiate(magicBall);
        obj.transform.position = this.transform.position;
        obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 700.0f));
        enemyEn.AddjustCurrentHealth((int)-magic / 3);
    }

    IEnumerator WaitToAttack01(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.transform.position = this.spawnPoint;
    }

    IEnumerator WaitToDefense(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.boxColl.enabled = true;
        shield.SetActive(false);
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
        StartCoroutine(Move(spawnPoint));
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
            this.magicBar.transform.localScale = new Vector3(this.HBLength * (this.magic / (float)maxMagic), this.HBLength, this.HBLength);
            yield return null;
        }
        Debug.Log("END");
        yield return null;
    }
}
