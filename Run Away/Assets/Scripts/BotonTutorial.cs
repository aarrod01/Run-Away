using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class BotonTutorial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Dentro", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Dentro", false);
    }

}
