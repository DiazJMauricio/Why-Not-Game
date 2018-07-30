using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patron de Movimiento",menuName = "Patrones/Patron de Movimiento ")]
[System.Serializable]
public class PatronMovimiento : ScriptableObject {

    public List<Movimiento> Movimientos = new List<Movimiento>();
}
