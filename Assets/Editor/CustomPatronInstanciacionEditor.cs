using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatronInstanciacion))]
public class CustomPatronInstanciacionEditor : Editor {

    PatronInstanciacion db;

    private void OnEnable()
    {
        db = (PatronInstanciacion)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("PATRON DE DISPARO");

        GUILayout.Label("Cantidad Disparos: " + db.instanciaciones.Count);
        GUILayout.Space(5);
        if (GUILayout.Button("Agregar Disparo")) AddDisparo();
        GUILayout.Space(20);


        for (int i = 0; i < db.instanciaciones.Count; i++) {
            //////
            GUILayout.BeginVertical();
                //////
                GUILayout.BeginHorizontal();
                    GUILayout.Label("Disparo " + (i+1));
                GUILayout.EndHorizontal();
                //////
                GUILayout.BeginHorizontal();

                    GUILayout.Label("Esperar Segundos");
                    db.instanciaciones[i].waitSeg = EditorGUILayout.FloatField(db.instanciaciones[i].waitSeg);
                    GUILayout.Label("Repetir");
                    db.instanciaciones[i].repeticiones = EditorGUILayout.IntField(db.instanciaciones[i].repeticiones);

                GUILayout.EndHorizontal();
                //////
                GUILayout.BeginHorizontal();    // <!--

                        GUILayout.Label("Bullet", GUILayout.Width(120));
                        db.instanciaciones[i].objInstanciable = (ObjInstanciable)EditorGUILayout.ObjectField(db.instanciaciones[i].objInstanciable, typeof(ObjInstanciable), true);

                GUILayout.EndHorizontal();      // -->
                //////
                GUILayout.BeginHorizontal();    // <!--
                    
                        GUILayout.Label("Rotacion", GUILayout.Width(100));
                        db.instanciaciones[i].rotacion = EditorGUILayout.FloatField("", db.instanciaciones[i].rotacion, GUILayout.Width(40));

                        GUILayout.Label("Velocidad", GUILayout.Width(100));
                        db.instanciaciones[i].velocidad = EditorGUILayout.FloatField("", db.instanciaciones[i].velocidad, GUILayout.Width(40));
                GUILayout.EndHorizontal();      // -->
                ///////
                //////
                GUILayout.BeginHorizontal();    // <!--
                    if (GUILayout.Button("Borrar"))
                    {
                        RemoveDisparo(i);
                        return;
                    }
                    if (GUILayout.Button("Duplicar"))
                    {
                        DuplicarDisparo(i, db.instanciaciones[i]);
                        return;
                    }

            GUILayout.EndHorizontal();      // -->
                ///////
                
           
            GUILayout.EndVertical();
            ////////
            GUILayout.Space(20);
        }
            //base.OnInspectorGUI();
    }
    public void AddDisparo() {
        db.instanciaciones.Add(new Instanciacion());
    }
    public void DuplicarDisparo(int index, Instanciacion ins) {
        Instanciacion ninst = new Instanciacion();
        ninst.direccion = ins.direccion;
        ninst.objInstanciable = ins.objInstanciable;
        ninst.repeticiones = ins.repeticiones;
        ninst.rotacion = ins.rotacion;
        ninst.velocidad = ins.velocidad;
        ninst.waitSeg = ins.waitSeg;


        db.instanciaciones.Insert(index+1, ninst);
    }
    void RemoveDisparo(int index)
    {
        db.instanciaciones.RemoveAt(index);
    }
}
