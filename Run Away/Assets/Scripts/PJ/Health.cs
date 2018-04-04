using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int health = 1;

    public void Danyar(int danyo)
    {
        health -= danyo;
        if (health <= 0)
            Destroy(gameObject);
    }

}
