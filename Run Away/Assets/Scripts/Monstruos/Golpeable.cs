using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]
public class Golpeable : MonoBehaviour {
    Interactuable master;
    Rigidbody2D monstruoRB;
    MonsterMovement monstruo;
    Health vida;
    public float distanciaInteraccion = 1f;
    public float anguloMaximoInteraccion = 45f;
    // Use this for initialization
    void Start()
    {
        master = GetComponent<Interactuable>();
        monstruoRB = GetComponentInParent<Rigidbody2D>();
        monstruo = GetComponentInParent<MonsterMovement>();
        vida= GetComponentInParent<Health>();
        master.Click = (PlayerMovement a) => {
            if ((monstruoRB.position - a.GetComponent<Rigidbody2D>().position).sqrMagnitude <= distanciaInteraccion
            && Mathf.DeltaAngle(monstruoRB.rotation, a.GetComponent<Rigidbody2D>().rotation) <= anguloMaximoInteraccion)
            {
                vida.Danyar(1);
                monstruo.Empujar(a.GetComponent<Rigidbody2D>().position, 2f);
            }
			
        };
        
       
    }

}
