using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoFruta : MonoBehaviour{
	public GameObject fruta;

    void Start(){
        
    }

    void Update(){
        fruta.transform.Rotate(Vector3.down, 0.5f);
    }

	private void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "piso"){
			Destroy(this.gameObject);
		}
	}

	public void tocar(){
		Destroy(this.gameObject);
		Puntos.instancia.sumar();
		MensajeEmergente.instancia.cambiarTexto("Muy bien!");
	}
}
