using UnityEngine;
using System.Collections;

public class RandomSelection : MonoBehaviour {
    public Sprite[] images;
    private SpriteRenderer sr;

	void Start () {
        int rnd = Random.Range(0, images.Length);
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = images[rnd];
    }
}
