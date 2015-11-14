using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void characterSelection(int faction) {
        PlayerPrefs.SetInt("FACTION", faction);
        if (faction == 1)
        {
            PlayerPrefs.SetFloat("MELEE", 1F);
            PlayerPrefs.SetFloat("MAGIC", 1F);
            PlayerPrefs.SetFloat("DEFENSE", 2F);
        }
        else if (faction == 2) {
            PlayerPrefs.SetFloat("MELEE", 0F);
            PlayerPrefs.SetFloat("MAGIC", 1F);
            PlayerPrefs.SetFloat("DEFENSE", 1F);
        }
        else if (faction == 3)
        {
            PlayerPrefs.SetFloat("MELEE", 1F);
            PlayerPrefs.SetFloat("MAGIC", 1F);
            PlayerPrefs.SetFloat("DEFENSE", 1F);
        }

        PlayerPrefs.SetInt("LEVEL",1);
        //Go to another scene...
    }
}
