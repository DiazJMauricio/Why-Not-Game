using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICatchDrop {

    int CatchDrop(IDrop drop);

}

public interface IDrop {

    void EfectoDrop();

}

public interface IShoot {

    void AtackManager();
    void Disparar(float time, GameObject bullet, Transform pos, float rotacion = 0);

}
