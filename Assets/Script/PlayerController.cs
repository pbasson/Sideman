using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public AudioClip jumpSF;
    public float moveSpeed = 0.1f;
    public int jumpVal = 750;

    private bool grounded = false;
    private Transform groundCheck;
    //  private Animator anim;
    private bool groundEnemy = false;
    private IntVec2 pos;


    void Start()
    {
        //anim = GetComponent<Animator>();
        pos = Graph.size;
        groundCheck = transform.Find("groundCheck");
    }

    void Update()
    {
      float hor = Input.GetAxis("Horizontal");

      if (Input.GetKey(KeyCode.D))
      {
        transform.Translate(new Vector3(moveSpeed, 0f, 0f));
      }

      if (Input.GetKey(KeyCode.A))
      {
        transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
      }

      if (hor >0 && !facingRight) {
          Flip(); }

      else if(hor< 0 && facingRight){
          Flip(); }


      grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

      if ( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) )
      {
        if (grounded)
        {
            AudioSource.PlayClipAtPoint(jumpSF, transform.position);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpVal));
        }
      }


    }

    void Flip()
    {
      facingRight = !facingRight;
      Vector3 theScale = transform.localScale;
      theScale.x *= -1;
      transform.localScale = theScale;
    }

    void LateUpdate()
    {
        Vector3 targetCam = new Vector3();
        targetCam.x = Mathf.Clamp(transform.position.x, 8 ,pos.x*11.5f-10) ;
        targetCam.y = Mathf.Clamp(transform.position.y,7,pos.y*11.5f-10);
        targetCam.z = -10;
        Camera.main.transform.position = targetCam;
    }
}
