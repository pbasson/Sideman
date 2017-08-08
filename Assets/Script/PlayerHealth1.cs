using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth1 : MonoBehaviour {

  public int health = 100;
  public Slider healthSlider;
	public float repeatDamage = 0f;
  public float hurtForce = 10f;
  public int damageAmount = 10;
  public Text loseText;

	private float lastTime;
  private Vector3 healthScale;
  private PlayerController playerControl;


void Awake()
{
    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<Slider>();
    loseText.enabled = false;
    healthSlider.value = health;
		playerControl = GetComponent<PlayerController>();
}



void TakeDamage(Transform enemy)
{
    Vector3 hurtVector = transform.position - enemy.position + Vector3.up*0.5f;
    GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
    health -= damageAmount;
    healthSlider.value = health;
}

void SetText()
{
    if (health <= 0)
    {
        StartCoroutine(End());
        loseText.enabled = true;
        loseText.text = "You Lose";
        Destroy(gameObject);
    }
}

IEnumerator End()
{
    yield return new WaitForEndOfFrame();
    SceneManager.LoadScene("Menu");
}

  void OnCollisionEnter2D(Collision2D col)
  {
    if(col.gameObject.tag == "Enemy")
    {
      if (health > 0)
      {
          TakeDamage(col.transform);
          SetText();
      }
    }
  }
}
