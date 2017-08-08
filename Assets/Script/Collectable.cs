using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public AudioClip collectSX;

    private float h = 0;

    void Update()
    {
        h *= Time.deltaTime;
				transform.Rotate(new Vector3(0f,0f,-30f*Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      if (col.tag == "Player")
      {
        AudioSource.PlayClipAtPoint(collectSX, transform.position);
        Destroy(transform.gameObject);
			}
		}
}
