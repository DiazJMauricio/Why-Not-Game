using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patron de Disparo", menuName = "Patrones/Patron de Disparo ")]
[System.Serializable]
public class PatronInstanciacion : ScriptableObject {

    public List<Instanciacion> instanciaciones = new List<Instanciacion>();
}
