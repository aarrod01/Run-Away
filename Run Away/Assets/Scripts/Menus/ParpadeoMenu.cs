using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoMenu : StateMachineBehaviour {
    
	public float frecuenciaMinima;
	public float frecuenciaMaxima;


    IEnumerator Animacion(Animator animator)
    {
        float time = Time.time;
		float frequency = Random.Range (frecuenciaMinima, frecuenciaMaxima);
		while (Time.time - time < frequency)
			yield return false;
		animator.SetTrigger ("parpadeo");
		yield return true;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Animacion (animator);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
