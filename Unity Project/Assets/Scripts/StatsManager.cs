using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {
        
    //Base stats
    private float melee;
    private float magic;
    private float defense;
    //On level up pair
    public float meleePair;
    public float magicPair;
    public float defensePair;
    //On level up non
    public float meleeNon;
    public float magicNon;
    public float defenseNon;
    //Character Level
    public float characterLevel;
    // Use this for initialization
    void Start () {
        melee = PlayerPrefs.GetFloat("MELEE");
        magic = PlayerPrefs.GetFloat("MAGIC");
        defense = PlayerPrefs.GetFloat("DEFENSE");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CalculateStat() {
    }
}
