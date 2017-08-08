using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour {

    public AudioClip bombSF;

    void BombSound()
    {
		  AudioSource.PlayClipAtPoint(bombSF, transform.position);
	  }

	void OnCollisionEnter2D(Collision2D col)
  {
		if (col.gameObject.CompareTag("Player"))
    {
				BombSound();
				Destroy(transform.gameObject);
    }
	}
}
