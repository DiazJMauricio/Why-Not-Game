using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergiaDrop : MonoBehaviour {

    public float velocidad;
    public float tiempoDespawn = 5;

    private PlayerController playerController;
    private Vector3 playerDirection;
    private float direcion;
    float r;


    /// FUNCIONES MONOBEHAVIOUR
    void Start () {
        r = Random.Range(1.0f, 2f);
        playerController = FindObjectOfType<PlayerController>();

        direcion = Random.Range(0f, 360f);
        transform.rotation =  Quaternion.Euler(0, 0, direcion);

        StartCoroutine(_Movimiento());
        StartCoroutine(_Despawn());
    }
	
	void Update () {
        transform.position += new Vector3((-3 + r) * Time.deltaTime, 0, 0);
    }
    private void OnTriggerStay2D(Collider2D collision) {
       
        if (collision.tag == "Player") {
            Vector3 playerPosition = collision.transform.position;

            playerDirection = playerPosition - transform.position;
            float distancia = playerDirection.magnitude;
            playerDirection = playerDirection / distancia;

            DarEnergiaAPlayerYDestruir(playerPosition);

            
            transform.position += playerDirection * (velocidad + (5-distancia)) * Time.fixedDeltaTime;
        }
       
    }


    /// FUNCIONES PROPIAS

    private void DarEnergiaAPlayerYDestruir(Vector3 playerPosition) {
        //  [si] este objeto esta en la posicion del player con un rango de 0.1f;
        if (transform.position.x <= playerPosition.x + 0.3f &&
            transform.position.x > playerPosition.x - 0.3f &&
            transform.position.y <= playerPosition.y + 0.3f &&
            transform.position.y > playerPosition.y - 0.3f) {

            playerController.CargarEnergia();
            Destroy(gameObject);
        }
    }

    /// CORRUTINAS

    IEnumerator _Movimiento() {
        for (int i = 0; i < 15; i++) {
            transform.position += transform.right * ((5 + Random.Range(-3, 3)) - i/3) * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator _Despawn() {
        yield return new WaitForSeconds(tiempoDespawn);
        Destroy(gameObject);
    }
}
