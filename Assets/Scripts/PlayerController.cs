using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int vidas;
    public int maxVidas = 3;

    public int maxEnergia = 15;
    public int energia;


    public bool invulnerable = false;
    Color colorOriginal;

    // Use this for initialization
    void Start () {
        vidas = maxVidas;
        colorOriginal = gameObject.GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        if (energia > maxEnergia) energia = maxEnergia;
        if (vidas > maxVidas) vidas = maxVidas;
        
	}


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBullet" && !invulnerable) {
            Hit();
        }
    }

    public void Hit() {
        StartCoroutine("_Hit");
    }

    public void Defuncion() {
        Destroy(gameObject);
    }

    IEnumerator _Hit() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ShakeCamara shake = FindObjectOfType<ShakeCamara>();
        shake._ShakeCamara(0.5f);
        vidas--;
        if (vidas <= 0) {
            Debug.Log("GAME OVER");
        }

        invulnerable = true;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = colorOriginal;
        invulnerable = false;

        yield return null;
    }
}
