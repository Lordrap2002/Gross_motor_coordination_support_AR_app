using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Clase que maneja el comportamiento de una fruta en el juego.
 * Esta clase se encarga de controlar la rotación de la fruta, detectar colisiones con el piso,
 * y activar el mensaje emergente al ser tocada.
 */
public class ComportamientoFruta : MonoBehaviour{
	public GameObject fruta, interfaz;
	public TextMeshPro mensaje;

    /**
     * Inicializa la interfaz y el mensaje emergente.
     */
    void Start(){
        interfaz = GameObject.Find("MensajeEmergente");
        mensaje = interfaz.GetComponent<TextMeshPro>();
    }

    /**
     * Actualiza la rotación de la fruta.
     */
    void Update(){
        fruta.transform.Rotate(Vector3.down, 0.5f);
    }

    /**
     * Detecta colisiones con el piso y elimina la fruta si lo hace.
     */
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "piso"){
            Destroy(this.gameObject);
        }
    }

    /**
     * Elimina la fruta, suma puntos y cambia el texto del mensaje emergente.
     */
    public void tocar(){
        Destroy(this.gameObject);
        SistemaPuntos.instancia.Sumar();
        mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Muy bien!");
    }
}
