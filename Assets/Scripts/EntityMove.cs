using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour {

    public PatronMovimiento pattern;
    public Color gizmosColor;
    public bool bucle;
    [ExecuteInEditMode]
    public Vector2 inversion = new Vector2(1,1);

    [Header("Variables para Desarrollo")]
    public bool DebugTime;
    public bool watchGizmos;

    private int currentMov = 1;
    List<Vector3> moveList = new List<Vector3>();

    private void Start()
    {
        foreach (var movimiento in pattern.Movimientos)
        {
            if (movimiento.tipoMovimiento == Movimiento.TipoMovimiento.toPosition)
            {
                moveList.Add(new Vector3(movimiento.vectorDireccion.x * inversion.x, movimiento.vectorDireccion.y * inversion.y, 0));
            }
        }
        transform.position = moveList[0];
    }

    private void OnDrawGizmos()
    {
        if (watchGizmos)
        {
            List<Vector3> gizmoP = new List<Vector3>();
            for (int i = 0; i < pattern.Movimientos.Count; i++)
            {
                gizmoP.Add(new Vector3(pattern.Movimientos[i].vectorDireccion.x * inversion.x, pattern.Movimientos[i].vectorDireccion.y * inversion.y, 0));
                Gizmos.color = gizmosColor;
                if (pattern.Movimientos[i].tipoMovimiento == Movimiento.TipoMovimiento.toPosition)
                {
                    Gizmos.DrawSphere(gizmoP[i], 0.1f);
                }
                if (i != 0)
                {
                    Gizmos.DrawLine(gizmoP[i - 1], gizmoP[i]);
                }
            }
            Gizmos.DrawSphere(gizmoP[0], 0.3f);
            Gizmos.DrawSphere(gizmoP[gizmoP.Count - 1], 0.2f);
            if (bucle)
            {
                Gizmos.DrawLine(gizmoP[gizmoP.Count - 1], gizmoP[0]);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, moveList[currentMov], pattern.Movimientos[currentMov].velocidad * Time.deltaTime);

        if (transform.position == moveList[currentMov]) {
            if (currentMov < moveList.Count-1) {
                currentMov++;
            } else if (bucle) {
                currentMov = 0;
            }
            if (DebugTime) Debug.Log(gameObject.name + "/EntityMove/CambioDeDireccion en: "+ Time.time);
        }

        if (GetComponent<Entity>() == null)
        {
            if (FueraDeCuadro())
            {
                Destroy(gameObject);
            }
        }
    }

    //  Devuelve true si el objeto sale de los limites permitidos.
    public bool FueraDeCuadro()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        return (transform.position.x > max.x + 5 || transform.position.y > max.y + 5 || transform.position.x < min.x - 5 || transform.position.y < min.y - 5);
    }
}
