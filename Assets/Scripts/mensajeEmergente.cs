using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MensajeEmergente : MonoBehaviour{
	public TextMeshPro texto;

    void Start(){

    }

    void Update(){
        
    }

	public void cambiarTexto(string mensaje){
		texto.text = mensaje;
	}
}
