using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {

	

}

public interface IHaveHealth {

    int MaxHealth { get; }
    int Health { get; set; }
    int ModificarHealth(int cant);

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
public interface IAmAShoot {
    int Daño { get; set; }
}
