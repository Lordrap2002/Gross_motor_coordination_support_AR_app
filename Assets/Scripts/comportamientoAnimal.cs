using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Microsoft.MixedReality.GraphicsTools.MeshInstancer;

public class ComportamientoAnimal : MonoBehaviour{
	public Transform camara;
	private AudioSource sonido;

	public void Awake(){
		sonido = GetComponent<AudioSource>();
	}

    void Start(){
        camara = Camera.main.transform;
    }

    void Update(){
		Vector3 direccion = camara.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 180f + Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg, 0);   
    }

	public void emitirSonido(){
		sonido.Play();
    }
}
