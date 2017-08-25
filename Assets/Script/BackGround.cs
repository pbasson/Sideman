using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGround : MonoBehaviour {


	// Use this for initialization
	void Start ()
	{

	}

    // Update is called once per frame
  void Update()
  {
		float sinT = Mathf.Sin(Time.time );
		transform.position = new Vector2(0, sinT*10f);

  }


}
