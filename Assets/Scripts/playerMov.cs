using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMov : MonoBehaviour {
    Vector2 move;
    Transform miTransform;

    public PlayerController playerController;

    public float velocidad;
    public float margenHor;
    public float margenVer;

    private bool DashDisponible     =   true;
    public float dashCooldown       =   .5f;
    public float dashMultiplicador  =   4;

	void Awake(){
        miTransform = this.GetComponent<Transform>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

	void Update(){

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2 (x, y);

        Mover(direction);

        // Dash
        if (Input.GetButtonDown("Dash")) {
            StartCoroutine(Dash());
        }
		
	}
    
    void Mover(Vector2 direccion) {
        //Limites de la camara.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - margenHor;
        min.x = min.x + margenHor;

        max.y = max.y - margenVer;
        min.y = min.y + margenVer;

        Vector2 pos = transform.position;
        pos += direccion * velocidad * Time.deltaTime;

        // coloca los limites de movimiento
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        miTransform.position = pos;
    }
    IEnumerator Dash() {
        
        if (DashDisponible) {

            DashDisponible = false;
            playerController.invulnerable = true;
            velocidad *= dashMultiplicador;


            yield return new WaitForSeconds(.1f);
            velocidad /= dashMultiplicador;
            playerController.invulnerable = false;

            yield return new WaitForSeconds(dashCooldown - 0.1f);
            DashDisponible = true;
            
        }
    }
}