using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

//  ENEMY CLASS
//  
//  Clase para objetos como naves enemigas o balas que sigan un patron de movimiento.

public class Enemy : MonoBehaviour {
    /// INDICE
    /// - Variables.
    /// 
    /// - Funciones MonoBehaviour.
    ///     - Start
    ///     - Update
    ///     - OnDestroy
    ///     - OnTriggerEnter2D
    ///     
    /// - Funciones MonoBehaviour alternativas.
    ///     - Padre Start
    ///     - Padre Update
    ///     
    /// - Funciones Propias.
    ///     - SetCambiosDeMovimiento
    ///     - MoveManager
    ///     - Disparar
    ///     - Hit
    ///     - Defuncion
    ///     - DropEnergia
    ///     
    /// - Corrutinas.
    ///     - _Hit


    /// VARIABLES
    //  Campos.
    public int maxVidas = 1;                        //  <- Cantidad de vidas del objeto.
    public bool objetoDeNivel;                      //  <- Define si es relevante para activar la siguiente instancia del nivel.
    public PatronMovimiento patronMovimiento;       //  <- Lista de movimientos y cuando ejecutarlos.
    public GameObject drop;                         //  <- Objeto instanciado al disminuir todas las vidas del objeto
    public int cantDrop;                            //  <- Cantidad de instanciaciones al morir.
    public Vector2 inversion = new Vector2(1, 1);   //  <- Cambia el lugar de inicio del objeto y su relacion de movimiento. 


    //  Propiedades.

    private int vidas;                              //  <- puntos de vida.
    private Vector3 nextPosition;                   //  <- Define la posicion del objeto.
    private Vector3 startPosition;                  //  <- Posicion inicial.
    private Vector3 playerPosition;                 //  <- Posicion del objeto Player.
    private Vector3 playerDirection;                //  <- Vector que apunta al objeto player.
    protected int movimientoActual = 0;

    [HideInInspector]  public float personalTimer;  //  <- Timer que cuenta el tiempo de vida de este objeto.
    [HideInInspector]
    public int numeroDeInstanciaDelNivel;           //  <- A que instancia del nivel pertenece este objeto.

    //  v-  Almacena los segundos en los que se produce un Cambio de movimiento en el patron de movimiento.
    List<float> cambiosDeMovimiento = new List<float>();
    private GameManager gameManager;


    /// FUNCIONES MONOBEHAVIOUR
    protected virtual void Start() {
        PadreStar();
    }
    protected virtual void Update() {
        PadreUpdate();
    }
    private void OnDestroy() {
        if (this != null) {
            if (objetoDeNivel && gameManager != null) {
                gameManager.InformarDefuncion(numeroDeInstanciaDelNivel);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.tag == "Enemy" && collision.gameObject.tag == "PlayerBullet") {
            Hit();
        }
        if (this.tag == "Enemy" && collision.gameObject.tag == "PlayerEspecialBullet") {
            Hit();
        }
        if (this.tag == "EnemyBullet" && collision.gameObject.tag == "Player") {
            Defuncion();
        }
        if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Enemy") {
            Defuncion();
        }
    }

    /// FUNCIONES MONOBEHAVIOUR ALTERNATIVAS
 
    //  Remplaza la funcion Start() 
    //  llamada por las clases que hereden de esta clase.
    public void PadreStar() {
        // v- Inicializacion de Variables.
        vidas = maxVidas;
        personalTimer = 0f;                  
        nextPosition = transform.position;     
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameManager = FindObjectOfType<GameManager>();
        startPosition = this.transform.position;
       
        playerDirection = playerPosition - startPosition;   //  <-|
        float distancia = playerDirection.magnitude;        //    | Inicializa PlayerDireccion
        playerDirection = playerDirection / distancia;      //  <-|

        SetCambiosDeMovimiento();               //  <- Llena la Lista CambiosDeMovimiento.
    }



    //  Remplaza la funcion Update()
    //  llamada por las clases que hereden de esta clase.
    public void PadreUpdate() {
        if (!GameManager.pause)
        {
            //  Timer
            personalTimer += Mathf.Round(Time.fixedDeltaTime * 100) / 100;

            //  Posicion.
            transform.position = new Vector3(nextPosition.x * inversion.x, nextPosition.y * inversion.y, 0);

            //  Patron de Movimiento.
            MoveManager();

            //  Limites de la camara.
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //  Elimina este objeto si sale de los limites permitidos.
            if (transform.position.x > max.x + 5 || transform.position.y > max.y + 5 || transform.position.x < min.x - 5 || transform.position.y < min.y - 5) {
                Destroy(gameObject);
            }
            //  Elimina el Objeto.
            if (vidas == 0) {
                Defuncion();
            }
        }
    }



    /// FUNCIONES PROPIAS

    //  Llena la lista cambiosDeMovimiento con los segundos en los 
    //  que se produce un cambio de movimiento en el patron de movimiento
    public void SetCambiosDeMovimiento() {
        float cambio = 0;
        cambiosDeMovimiento.Add(cambio);
        for (int i = 0; i < patronMovimiento.Movimientos.Count; i++){
            cambio += patronMovimiento.Movimientos[i].duracion;
            cambiosDeMovimiento.Add(cambio);
        }
    }

    //  Interpreta el Patron de movimiento actualizando la variable 'nextPosition'
    //   que controla la posicion de este objeto
    public void MoveManager() {
        for (int i = movimientoActual; i < cambiosDeMovimiento.Count-1; i++) {
            if (cambiosDeMovimiento[i] == Mathf.Round( personalTimer * 100 ) / 100) {
                movimientoActual = i;
                break;
            }
        }
        switch (patronMovimiento.Movimientos[movimientoActual].tipoMovimiento) {
            case Movimiento.TipoMovimiento.Vectorial:
                nextPosition += patronMovimiento.Movimientos[movimientoActual].vectorDireccion * Time.fixedDeltaTime;
                break;
            case Movimiento.TipoMovimiento.Arco:
                break;
            case Movimiento.TipoMovimiento.toPosition:
                nextPosition = Vector3.MoveTowards(nextPosition, patronMovimiento.Movimientos[movimientoActual].vectorDireccion, patronMovimiento.Movimientos[movimientoActual].velocidad * Time.fixedDeltaTime);
                break;
            case Movimiento.TipoMovimiento.DireccionAlPlayer:
                nextPosition += playerDirection * patronMovimiento.Movimientos[movimientoActual].velocidad * Time.fixedDeltaTime;
                break;
            case Movimiento.TipoMovimiento.Derecha:
                nextPosition += transform.right * patronMovimiento.Movimientos[movimientoActual].velocidad * Time.fixedDeltaTime;
                break;
            default:
                break;
        }
    }

    protected void _reiniciarCiclo(float time, float volverA) {
        personalTimer = Mathf.Round(personalTimer * 100) / 100;
        time = Mathf.Round(time * 100) / 100;
        if (personalTimer == time) {
            movimientoActual = 1;
            personalTimer = volverA;
            
        }
    }

    //  Instancia un objeto con ciertas propiedades.
    public void Disparar(float time, GameObject bullet,Transform pos, float rotacion = 0) {
        if (!GameManager.pause)
        {
            if (_controlDisparo(time))
            {
                rotacion = _presisarRotacion(rotacion);
                GameObject objeto = _InstanciarEnemyBullet(bullet);
                _setPositionYRotacionEnemyBullet(objeto, rotacion, pos);
            }
        }
    }
        /// funciones para Disparar
        bool _controlDisparo(float time) {
            personalTimer = Mathf.Round(personalTimer * 100) / 100;
            time = Mathf.Round(time * 100) / 100;
            return (personalTimer == time);
        }
        float _presisarRotacion(float r) {
            if (inversion.y < 0) r += (180 - r) * 2;
            if (inversion.x < 0) r += (180 - r) * 2 + 180;
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
    
    //  Inicializa la Corrutina '_Hit'
    public void Hit() {
        StartCoroutine("_Hit");
    }

    //  Mata este Objeto y instancia el drop.
    public void Defuncion() {
        for (int i = 0; i < cantDrop; i++) {
            DropEnergia();
        }
        Destroy(gameObject);
    }

    //  Instancia un Drop.
    public void DropEnergia() {
        GameObject drop1 = Instantiate(drop).gameObject;
        drop1.transform.position = transform.position;
    }

    /// CURRUTINAS
    //  Resivir Daño.
    IEnumerator _Hit() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

        vidas--;
        yield return null;
    }
}
