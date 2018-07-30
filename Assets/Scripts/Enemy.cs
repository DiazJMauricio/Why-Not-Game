using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Enemy : MonoBehaviour {
    /// INDICE
    /// - Variables.
    /// - Funciones MonoBehaviour.
    /// - Funciones MonoBehaviour alternativas.
    /// - Funciones Propias.
    /// - Corrutinas.


    /// VARIABLES

    public bool objetoDeNivel;          //  Define si es relevante para activar la siguiente instancia del nivel.
    public PatronMovimiento patronMovimiento;
    public GameObject drop;
    public int cantDrop;

    public int vidas;
    public int maxVidas = 1;

    Color colorOriginal;

    private Vector3 nextPosition;       //  Define la posicion del objeto.
    private Vector3 startPosition;
    private Vector3 playerPosition;
    private Vector3 playerDirection;

    [HideInInspector]  public float personalTimer;
    [HideInInspector]  public int numeroDeInstanciaDelNivel;

    List <float> cambiosDeMovimiento = new List<float>();

    //  Cambia el lugar de inicio del objeto y su relacion de movimiento. 
    public float inversionX = 1;        //  RECOMENDADO [1 o -1].
    public float inversionY = 1;        //  RECOMENDADO [1 o -1].


    /// FUNCIONES MONOBEHAVIOUR
    public void Start() {
        PadreStar();
    }
    public void Update() {
        PadreUpdate();
    }
    private void OnDestroy() {
        if (this != null) {
            if (objetoDeNivel) {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.InformarDefuncion(numeroDeInstanciaDelNivel);
            }
        } else {
            Debug.Log("s");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.tag == "Enemy" && collision.gameObject.tag == "PlayerBullet") {
            Hit();
        }
    }

    /// FUNCIONES MONOBEHAVIOUR ALTERNATIVAS
    //  !- Remplaza la funcion Start() para ser llamada por las clases que hereden de esta clase.
    public void PadreStar() {
        vidas = maxVidas;

        colorOriginal = gameObject.GetComponent<SpriteRenderer>().color;

        personalTimer = 0f;
        //  Posicion del objeto.
        nextPosition = transform.position;
        SetCambiosDeMovimiento();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        startPosition = this.transform.position;
        playerDirection = playerPosition - startPosition;
        float distancia = playerDirection.magnitude;
        playerDirection = playerDirection / distancia;
    }

    //  !- Remplaza la funcion Update() para ser llamada por las clases que hereden de esta clase.
    public void PadreUpdate() {
        personalTimer += Mathf.Round(Time.fixedDeltaTime * 100) / 100;
        //  Posicion del objeto.
        transform.position = new Vector3(nextPosition.x * inversionX, nextPosition.y * inversionY, 0);

        MoveManager();

        //  Limites de la camara.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //  Elimina este objeto si sale de los limites permitidos.
        if (transform.position.x > max.x + 5 || transform.position.y > max.y + 5 || transform.position.x < min.x - 5 || transform.position.y < min.y - 5) {
            Destroy(gameObject);
        }

        if (vidas == 0) {
            Defuncion();
        }
    }


    /// FUNCIONES PROPIAS
    public void SetCambiosDeMovimiento() {
        float cambio = 0;
        cambiosDeMovimiento.Add(cambio);
        for (int i = 0; i < patronMovimiento.Movimientos.Count; i++){
            cambio += patronMovimiento.Movimientos[i].duracion;
            cambiosDeMovimiento.Add(cambio);
        }
    }
    public void MoveManager() {
        for (int i = 0; i < patronMovimiento.Movimientos.Count; i++) {
            if (personalTimer >= cambiosDeMovimiento[i] && personalTimer < cambiosDeMovimiento[i+1]) {
                switch (patronMovimiento.Movimientos[i].tipoMovimiento)
                {
                    case Movimiento.TipoMovimiento.Vectorial:
                        nextPosition += patronMovimiento.Movimientos[i].vectorDireccion * Time.fixedDeltaTime;
                        break;
                    case Movimiento.TipoMovimiento.Arco:
                        break;
                    case Movimiento.TipoMovimiento.DireccionAlPlayer:
                        nextPosition += playerDirection * patronMovimiento.Movimientos[i].velocidad * Time.fixedDeltaTime;
                        break;
                    case Movimiento.TipoMovimiento.Derecha:
                        nextPosition += transform.right * patronMovimiento.Movimientos[i].velocidad * Time.deltaTime;
                        break;
                    default:
                        break;
                } 
            }
        }
    }

    public void Disparar(float time, GameObject bullet,Transform pos, float rotacion = 0) {
        if (_controlDisparo(time)) {
            rotacion = _presisarRotacion(rotacion);
            GameObject objeto = _InstanciarEnemyBullet(bullet);
            _setPositionYRotacionEnemyBullet(objeto,rotacion, pos);
        }
    }
        /// funciones para Disparo
        bool _controlDisparo(float time) {
            personalTimer = Mathf.Round(personalTimer * 100) / 100;
            time = Mathf.Round(time * 100) / 100;
            return (personalTimer == time);
        }
        float _presisarRotacion(float r) {
            if (inversionY < 0) r += (180 - r) * 2;
            if (inversionX < 0) r += (180 - r) * 2 + 180;
            return r;
        }
        GameObject _InstanciarEnemyBullet(GameObject go) {
            GameObject objetoInstanciado = Instantiate(go).gameObject;
            objetoInstanciado.tag = "EnemyBullet";
            return objetoInstanciado;
        }
        void _setPositionYRotacionEnemyBullet(GameObject bullet,float rotacion, Transform position) {
            bullet.transform.position = position.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotacion);
        }
    
    public void Hit() {
        StartCoroutine("_Hit");
    }

    public void Defuncion() {
        for (int i = 0; i < cantDrop; i++) {
            DropEnergia();
        }
        Destroy(gameObject);
    }

    public void DropEnergia() {
        GameObject drop1 = Instantiate(drop).gameObject;
        drop1.transform.position = transform.position;
    }

    /// CURRUTINAS
    IEnumerator _Hit() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = colorOriginal;

        //yield return new WaitForSeconds(0.2f);
        vidas--;
        yield return null;
    }
}
