using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatronMovimiento))]
public class CustomPatronMovimientoEditor : Editor {

    PatronMovimiento db;

    private void OnEnable() {
        db = (PatronMovimiento)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("PATRON DE MOVIMIENTO");
        GUILayout.Label("Cantidad Movimientos: " + db.Movimientos.Count);
        GUILayout.Space(5);
        if (GUILayout.Button("Agregar Movimiento")) AddMovimiento();

        GUILayout.Space(10);

        for (int i = 0; i < db.Movimientos.Count; i++)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    GUILayout.Label((i+1) + " Tipo ");
                    db.Movimientos[i].tipoMovimiento = (Movimiento.TipoMovimiento)EditorGUILayout.EnumPopup(db.Movimientos[i].tipoMovimiento);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

            switch (db.Movimientos[i].tipoMovimiento)
            {
                case Movimiento.TipoMovimiento.Vectorial:
                    GUILayout.Label("Duracion");
                    db.Movimientos[i].duracion = EditorGUILayout.FloatField(db.Movimientos[i].duracion, GUILayout.Width(40));
                    GUILayout.Label("Direccion");
                    db.Movimientos[i].vectorDireccion = EditorGUILayout.Vector2Field("", db.Movimientos[i].vectorDireccion, GUILayout.Width(80));
                    break;
                case Movimiento.TipoMovimiento.Arco:
                    break;
                case Movimiento.TipoMovimiento.Derecha:
                    GUILayout.Label("Duracion");
                    db.Movimientos[i].duracion = EditorGUILayout.FloatField(db.Movimientos[i].duracion, GUILayout.Width(40));
                    GUILayout.Label("velocidad");
                    db.Movimientos[i].velocidad = EditorGUILayout.FloatField(db.Movimientos[i].velocidad, GUILayout.Width(40));
                    break;
                case Movimiento.TipoMovimiento.DireccionAlPlayer:
                    GUILayout.Label("Duracion");
                    db.Movimientos[i].duracion = EditorGUILayout.FloatField(db.Movimientos[i].duracion, GUILayout.Width(40));
                    GUILayout.Label("velocidad");
                    db.Movimientos[i].velocidad = EditorGUILayout.FloatField(db.Movimientos[i].velocidad, GUILayout.Width(40));
                    break;
                default:
                    break;
            }
            if (db.Movimientos[i].tipoMovimiento == Movimiento.TipoMovimiento.Vectorial) {
                    
                }

            

               
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            if (GUILayout.Button("Borrar", GUILayout.Height(35)))
            {
                RemoveMovimiento(i);
                return;
            }

            GUILayout.EndHorizontal();

        }

    }

    void AddMovimiento()
    {
        db.Movimientos.Add(new Movimiento());
    }
    void RemoveMovimiento(int index) {
        db.Movimientos.RemoveAt(index);
    }
}
