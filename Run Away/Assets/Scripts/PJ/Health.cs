using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int health = 1;
    MonsterMovement monstruo;

    void Start()
    {
        monstruo = GetComponent<MonsterMovement>();   
    }


    public void Danyar(int danyo)
    {
        health -= danyo;
        if (health <= 0)
        {
            Muerte();
        }
            
    }

    void Muerte()
    {
        if (monstruo == null)
            GameManager.instance.MonstruoMuerto(monstruo.tipo);
        Destroy(gameObject);
    }

}
