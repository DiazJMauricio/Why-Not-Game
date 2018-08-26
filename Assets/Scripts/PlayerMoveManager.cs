using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour {
    
    public float velocidad;
    public float dashCooldown;
    public float dashMultiplicador;
    public Vector2 margenes;

    [HideInInspector]
    public bool OnDash = false;
    public bool encerrar = false;

    private bool DashDisponible     =   true;
    private Health health;
    private ManagerGame managerGame;

    void Awake(){
        health = GetComponent<Health>();
        managerGame = FindObjectOfType<ManagerGame>();
        managerGame.LevelIntro += IntroPlayer;
        managerGame.LevelWin += DespedirPlayer;
    }

    void Update() {
        if (!ManagerGame.inPausa)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(x, y);

            Mover(direction);

            // Dash
            if (Input.GetButtonDown("Dash") && ManagerGame.onGame)
            {
                StartCoroutine(Dash());
            }
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

        if (encerrar) {
            // coloca los limites de movimiento
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);

            transform.position = pos;
        }
        
    }
    private void IntroPlayer() {
        StartCoroutine(_IntroPlayer());
    }
    private void DespedirPlayer()
    {
        StartCoroutine(_DespedirPlayer());
    }


    IEnumerator Dash() {
        
        if (DashDisponible) {

            DashDisponible = false;
            OnDash = true;
            health.invulnerable = true;
            velocidad *= dashMultiplicador;


            yield return new WaitForSeconds(.12f);
            velocidad /= dashMultiplicador;
            health.invulnerable = false;
            OnDash = false;

            yield return new WaitForSeconds(dashCooldown - 0.1f);
            DashDisponible = true;
            
        }
    }

    IEnumerator _IntroPlayer()
    {
        Vector3 centerPos = new Vector3(0, 0, 0);
        while (transform.position != centerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, centerPos, 3 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        encerrar = true;
    }

    IEnumerator _DespedirPlayer()
    {
        encerrar = false;
        yield return new WaitForSeconds(1f);
        Vector3 centerPos = new Vector3(10, 0, 0);
        for (float i = 0; i < 15; i += Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, centerPos, 5 * Time.deltaTime);
            yield return null;
        }
    }
}