using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nivel ", menuName = "Nivel")]
[System.Serializable]
public class Nivel : ScriptableObject {
    public List<FaseDeNivel> fasesDelNivel = new List<FaseDeNivel>();
}
