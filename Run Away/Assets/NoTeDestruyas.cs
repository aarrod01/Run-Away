using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTeDestruyas : MonoBehaviour {

	void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
