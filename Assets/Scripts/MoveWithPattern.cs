using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//v_0.0.2
//  Hacer que funcione independientemente
public class MoveWithPattern : Timer {

    public PatronMovimiento patronMovimiento;       //  <- Lista de movimientos y cuando ejecutarlos.
    public Vector2 inversion = new Vector2(1, 1);   //  <- Cambia el lugar de inicio del objeto y su relacion de movimiento. 


    private Vector3 nextPosition;                   //  <- Define la posicion del objeto.
    private Vector3 playerDirection;                //  <- Vector que apunta al objeto player.
    private Vector3 startPosition;                  //  <- Posicion inicial.
    private Vector3 playerPosition;                 //  <- Posicion del objeto Player.
    private int movimientoActual = 0;
    private List<float> cambiosDeMovimiento = new List<float>();


    private void Awake ()
    {
        if (patronMovimiento == null)  Debug.LogError("El Objeto " + gameObject.name + " no tiene instanciado un Patron de movimiento");

        
        SetSecondOnTimer(0f);
    }
    private void Start()
    {
        SetVariables();
    }

    void Update () {
        
        if (!ManagerGame.inPausa) {
            //  Patron de Movimiento.
            MoveManager();
            UpdateTimer();
            if (GetComponent<Entity>() == null)
            {
                if (FueraDeCuadro())
                {
                    Destroy(gameObject);
                }
            }
        }
    }




    public void SetVariables()
    {
        //  <Vector3> nextposition. / <Vector3> starPosition.
        nextPosition = startPosition = transform.position;

        //  <Vector3> PlayerPosition.
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        //  <Vector3> PlayerDirection.
        playerDirection = playerPosition - startPosition;   //  <-|
        float distancia = playerDirection.magnitude;        //    | Inicializa PlayerDireccion
        playerDirection = playerDirection / distancia;      //  <-|

        //  <List> CambiosDeMovimiento.
        float cambio = 0;
        cambiosDeMovimiento.Add(cambio);
        for (int i = 0; i < patronMovimiento.Movimientos.Count; i++)
        {
            cambio += patronMovimiento.Movimientos[i].duracion;
            cambiosDeMovimiento.Add(cambio);
        }
    }





    //  Interpreta el patron de movimiento.
    public void MoveManager()
    {
        //  Investiga que movimiento del patronDeMovimiento es el movimiento actual.
        for (int i = movimientoActual; i < cambiosDeMovimiento.Count - 1; i++)
        {
            if (cambiosDeMovimiento[i] == GetPersonalTimer())
            {
                movimientoActual = i;
                break;
            }
        }
        // interpreta el movimiento.
        switch (patronMovimiento.Movimientos[movimientoActual].tipoMovimiento)
        {
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
        //  Posicion.
        transform.position = new Vector3(nextPosition.x * inversion.x, nextPosition.y * inversion.y, 0);

    }





    public void SetMovimientoActual(int move) {
        movimientoActual = move;
    }

    //  Devuelve true si el objeto sale de los limites permitidos.
    public bool FueraDeCuadro()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        return (transform.position.x > max.x + 5 || transform.position.y > max.y + 5 || transform.position.x < min.x - 5 || transform.position.y < min.y - 5);
    }
}
