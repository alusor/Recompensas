using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //Player EXP
    private int EXP;
    private int Faction;
    private StatsManager stats;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("PLAYER_EXP"))
        {
            EXP = PlayerPrefs.GetInt("PLAYER_EXP");
        }
        else
        {
            EXP = 100;
            PlayerPrefs.SetInt("PLAYER_EXP", EXP);
        }
        Faction = PlayerPrefs.GetInt("FACTION");
        stats = GetComponent<StatsManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
