using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Movimiento {

    public enum TipoMovimiento { toPosition, Vectorial, Arco , DireccionAlPlayer, Derecha };
    public TipoMovimiento tipoMovimiento;

    public float duracion;
    public Vector3 vectorDireccion = new Vector3();
    public float velocidad;
}
