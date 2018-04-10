using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {

	public int vida = 1;
    MonsterMovement monstruo;

    void Start()
    {
        monstruo = GetComponent<MonsterMovement>();   
    }


    public void Danyar(int danyo)
    {
        vida -= danyo;
        if (vida <= 0)
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
