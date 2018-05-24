using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {

    public float velocidadMaxima;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.anyKey)
            animator.speed = velocidadMaxima;
        else
            animator.speed = 1;
    }

    public void Fin()
    {
        SceneManager.LoadScene("Inicio");
    }
}
