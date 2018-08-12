using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {
    Health Health { get; set; }
    MoveWithPattern MoveWithP { get; set; }
    int NumeroDeLaFasePerteneciente { get; set; }
    GameManager IGameManager { get; set; }
}

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

public interface IInstaciaDeFase {

    int NumeroDeInstanciaPerteneciente { get; set; }
    GameManager GameManager { get; set; }

}
