using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MensajeEmergente : MonoBehaviour{
	public static MensajeEmergente instancia;
	public TextMeshPro texto;

	public void Awake(){
		instancia = this;
	}

    void Start(){

    }

    void Update(){
        
    }

	public void cambiarTexto(string mensaje){
		texto.text = mensaje;
	}
}
