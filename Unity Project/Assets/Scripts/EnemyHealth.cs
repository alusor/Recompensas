using UnityEngine;
using System.Collections;

public class EnemyHealth : Enemy {

    public int maxHealth = 100;
    public int curHealth = 100;
    public GUIStyle lifeBar;


    private float healthBarLength;
    private GameObject enemy;
    private Object enemyType;
    // Use this for initialization
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        switch (enemy.name)
        {
            case "EnemyType01":
                break;
            case "EnemyType02":
                break;
            case "EnemyType03":
                break;
        }
        healthBarLength = Screen.width;

    }

    // Update is called once per frame
    void Update()
    {
        //curHealth = (int)enemy.GetComponent<Type01>().health;
        //AddjustCurrentHealth(-1);

    }

    void OnGUI()
    {

        GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth, lifeBar);

    }

    public void AddjustCurrentHealth(int adj)
    {

        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;


        healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);

    }
}
