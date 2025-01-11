using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Clase que maneja los mensajes emergentes en el juego.
 * Esta clase se encarga de mostrar mensajes emergentes en la pantalla del juego.
 */
public class MensajeEmergente : MonoBehaviour{
	public TextMeshPro texto;

    void Start(){

    }

    void Update(){
        
    }

    /**
     * Cambia el texto del mensaje emergente.
     */
    public void cambiarTexto(string mensaje){
        texto.text = mensaje;
    }
}
