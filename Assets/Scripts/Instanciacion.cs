using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Instanciacion {


    public float waitSeg;
    public int repeticiones;
    public ObjInstanciable objInstanciable;
    
    public float rotacion;
    public float velocidad;

    public Vector3 direccion = new Vector3();
}
