using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComportamientoFruta : MonoBehaviour{
	public GameObject fruta, interfaz;
	public TextMeshPro mensaje;

    void Start(){
        interfaz = GameObject.Find("MensajeEmergente");
		mensaje = interfaz.GetComponent<TextMeshPro>();
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
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Muy bien!");
	}
}
