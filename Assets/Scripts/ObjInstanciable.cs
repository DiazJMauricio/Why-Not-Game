using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

/// <summary>
/// ObjInstanciable son los objetos implicados en la jubalidad, como pueden ser Naves, proyectiles, obstaculos, etc.
/// Sus movimientos y patrones de ataque estan declarados por los Patrones.
/// Si este objeto es instanciado por otro ObjInstanciable, su posicion inicial sera la misma que el objeto que lo instancio al momento de instanciarlo,
/// caso contrario su posicion inicial sera la declarada en el prefab.
/// 
/// Al ser destruido este objeto se debe llamar al GameManager para que este incialice otra instancia o finalice en nivel
/// </summary>
public class ObjInstanciable : MonoBehaviour {

    // PATRONES Scriptable Objects.
    public PatronInstanciacion patronInstanciacion; //  Scriptable object que controla todas las instanciaciones de objetos que se realizan por este objeto.
    public PatronMovimiento patronMovimiento;       //  Scriptable object que controla el movimiento de este objeto.
    
    //  Define la posicion del objeto.
    private Vector3 nextPosition;

    //  Cambia el lugar de inicio del objeto.
    public bool invertirY;
    public bool invertirX;
    int invX = 1;
    int invY = 1;


    void Start () {
        //  Posicion del objeto.
        nextPosition = transform.position;

        //  Inicializa el Movimiento
        //if (patronMovimiento != null) StartCoroutine("Movimiento");
        Timing.RunCoroutine(_Movimiento(),Segment.FixedUpdate);

        //  Inicializa las instanciaciones
        if (patronInstanciacion != null) StartCoroutine("Instanciaciones");


        //  Cambia el lugar de inicio del objeto.
        if (invertirX) { invX = -1; }
        if (invertirY) { invY = -1; }

    }
	

	void FixedUpdate () {
        //  Posicion del objeto.
        transform.position = new Vector3(nextPosition.x * invX, nextPosition.y * invY, 0);

        //  Limites de la camara.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //  Elimina este objeto si sale de los limites permitidos.
        if (transform.position.x > max.x+5 || transform.position.y > max.y+5 || transform.position.x < min.x-5 || transform.position.y < min.y-5) {
            Destroy(gameObject);
        }
    }


    //  MUEVE AL OBJETO POR EL ESCENARIO.
    IEnumerator<float> _Movimiento()
    {
        

        //  Por cada [Movimiento] en [Patron Movimiento].
        for (int i = 0; i < patronMovimiento.Movimientos.Count; i++) {
            switch (patronMovimiento.Movimientos[i].tipoMovimiento) {

                //  Movimiento Vectorial
                case global::Movimiento.TipoMovimiento.Vectorial:
                    for (float e = 0; e < patronMovimiento.Movimientos[i].duracion; e += Time.deltaTime) {
                        nextPosition += patronMovimiento.Movimientos[i].vectorDireccion * Time.deltaTime;
                        yield return Timing.WaitForOneFrame;
                    }
                    break;


                //  Movimiento de arco
                case global::Movimiento.TipoMovimiento.Arco:          
                    break;


                //  Movimiento de Determinado por angulo
                case global::Movimiento.TipoMovimiento.Derecha:
                    for (float e = 0; e < patronMovimiento.Movimientos[i].duracion; e += Time.fixedDeltaTime) {
                        nextPosition += transform.right * patronMovimiento.Movimientos[i].velocidad * Time.fixedDeltaTime;
                        yield return Timing.WaitForOneFrame;
                    }
                    break;


                //  Movimiento hacia el player
                case global::Movimiento.TipoMovimiento.DireccionAlPlayer:
                    Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                    for (float e = 0; e < patronMovimiento.Movimientos[i].duracion; e += Time.deltaTime) {
                        nextPosition = Vector3.MoveTowards(nextPosition, playerPosition, patronMovimiento.Movimientos[i].velocidad * Time.deltaTime);
                        yield return Timing.WaitForOneFrame;
                    }
                    break;

                default:
                    break;
            }
        }
        yield return Timing.WaitForOneFrame;
    }


    //  INICIALIZA INSTANCIAS DEL PATRON DE INSTANCIACION.
    IEnumerator Instanciaciones()
    {
        //  Por cada [Instanciacion] en [Patron instanciacion]
        for (int instanciacion = 0; instanciacion < patronInstanciacion.instanciaciones.Count; instanciacion++) {

            //  Por cada [instanciacion.repetir]
            for (int i = 0; i < patronInstanciacion.instanciaciones[instanciacion].repeticiones; i++) {

                //  Tiempo de espera en segundos hasta instanciar
                yield return new WaitForSeconds(patronInstanciacion.instanciaciones[instanciacion].waitSeg);

                //  Enstancia el objeto declarado en [Instanciacion[d].objInstanciable]
                GameObject bullet1 = Instantiate(patronInstanciacion.instanciaciones[instanciacion].objInstanciable).gameObject;
                //  Estable la posicion y la rotacion del objeto instanciado.
                bullet1.transform.position = this.transform.position;
                bullet1.transform.rotation = Quaternion.Euler(0, 0, patronInstanciacion.instanciaciones[instanciacion].rotacion);

            }
            yield return null;
        }
    }
}
