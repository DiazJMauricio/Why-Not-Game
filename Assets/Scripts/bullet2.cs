using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour {
    public bool direccion = true;
    public Vector2 p;
    
	// Use this for initialization
	void Start () {
        if (direccion){
            p = new Vector2(0.03f + Random.Range(-0.02f, 0.02f), 20);
        }
        else {
            p = new Vector2(-0.03f + Random.Range(-0.02f, 0.02f), 20);
        }
        StartCoroutine(moverVertical(p));
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < min.x) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
    IEnumerator moverVertical(Vector2 v2)
    {
        //  v2.x = distancia por ciclo.
        //  v2.y = cantidad de ciclos.
        for (int i = 0; i < v2.y; i++)
        {
            transform.position = new Vector3(transform.position.x - (i * Time.deltaTime - 0.1f), transform.position.y + v2.x, 0);
            yield return null;
        }
        StartCoroutine(moverHorizontal(new Vector2(-0.2f, 500f)));
    }
    IEnumerator moverHorizontal(Vector2 v2)
    {
        //  v2.x = distancia por ciclo.
        //  v2.y = cantidad de ciclos.
        for (int i = 0; i < v2.y; i++)
        {
            transform.position = new Vector3(transform.position.x + v2.x, transform.position.y, 0);
            yield return null;
        }
    }
}
