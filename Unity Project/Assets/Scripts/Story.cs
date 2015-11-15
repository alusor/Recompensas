using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour
{
    private int translateDuration = 1;

	void Start ()
    {
        this.StartCoroutine(Wait(3.0f));
	}

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.StartCoroutine(this.Move(new Vector3(-5.75f, this.transform.position.y, this.transform.position.z)));

        yield return new WaitForSeconds(seconds + this.translateDuration);
        this.StartCoroutine(this.Move(new Vector3(-11.55f, this.transform.position.y, this.transform.position.z)));

        yield return new WaitForSeconds(seconds + this.translateDuration);
        Application.LoadLevel("battleScn");
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
        yield return null;
    }
}
