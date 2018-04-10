using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Run Away/Assets/Scripts/PJ/Vida.cs
public class Vida : MonoBehaviour {

	public int vida = 1;
    MonsterMovement monstruo;
=======
public class Health : MonoBehaviour 
{
    MonsterMovement monstruo;

	public int health = 1;
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0:Run Away/Assets/Scripts/PJ/Health.cs

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
