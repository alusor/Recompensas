using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void characterSelection(int faction) {
        PlayerPrefs.SetInt("FACTION", faction);
        PlayerPrefs.SetFloat("MELEE",1F);
        PlayerPrefs.SetFloat("MAGIC", 1F);
        PlayerPrefs.SetFloat("DEFENSE", 1F);

    }
}
