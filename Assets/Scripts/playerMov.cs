using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMov : MonoBehaviour {
    
    public float velocidad;
    public float dashCooldown;
    public float dashMultiplicador;
    public Vector2 margenes;

    private bool DashDisponible     =   true;
    private Transform miTransform;
    private PlayerController playerController;

    void Awake(){
        miTransform = GetComponent<Transform>();
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

        max.x = max.x - margenes.x;
        min.x = min.x + margenes.x;

        max.y = max.y - margenes.y;
        min.y = min.y + margenes.y;

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
            playerController.onDash = true;
            velocidad *= dashMultiplicador;


            yield return new WaitForSeconds(.12f);
            velocidad /= dashMultiplicador;
            playerController.invulnerable = false;
            playerController.onDash = false;

            yield return new WaitForSeconds(dashCooldown - 0.1f);
            DashDisponible = true;
            
        }
    }
}