using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Clase que maneja el temporizador del juego.
 * Esta clase se encarga de inicializar, actualizar y mostrar el tiempo restante en el juego.
 */
public class Temporizador : MonoBehaviour{
	public static Temporizador instancia;
	public TextMeshPro texto;
	public int tiempoMax = 30;
	public int tiempo;

	/**
	 * Inicializa la instancia estática de la clase.
	 */
	public void Awake(){
		instancia = this;
	}

    /**
     * Inicializa el tiempo restante y muestra el tiempo máximo al inicio del juego.
     */
    void Start(){
		tiempo = tiempoMax;
        texto.text = "Tiempo: " + tiempo.ToString();
    }

    void Update(){
       
    }

	/**
	 * Reinicia el tiempo restante al tiempo máximo.
	 */
	public void reiniciar(){
        tiempo = tiempoMax;
    }

	/**
	 * Resta un segundo al tiempo restante y actualiza el texto.
	 */
	public void restarSegundo(){
		tiempo -= (tiempo > 0 ? 1 : 0);
		texto.text = "Tiempo: " + tiempo.ToString();
	}
}
