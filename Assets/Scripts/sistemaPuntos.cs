using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Clase que maneja el sistema de puntos del juego.
 * Esta clase se encarga de mostrar el puntaje actual, reiniciar el puntaje y sumar puntos.
 */
public class SistemaPuntos : MonoBehaviour{
	public static SistemaPuntos instancia;
	public TextMeshPro texto;
	public int puntaje = 0;
	private AudioSource sonido;

	/**
	 * Inicializa la instancia estática y encuentra la fuente de sonido.
	 */
	public void Awake(){
		instancia = this;
		sonido = GetComponent<AudioSource>();
	}

    /**
     * Establece el texto del puntaje al iniciar el juego.
     */
    void Start(){
        texto.text = "Puntaje: " + puntaje.ToString();
    }

    void Update(){
        
    }

    /**
     * Reinicia el puntaje a cero y actualiza el texto.
     */
    public void Reiniciar(){
        puntaje = 0;
        texto.text = "Puntaje: " + puntaje.ToString();
    }

    /**
     * Suma un punto al puntaje actual, actualiza el texto y reproduce un sonido.
     */
    public void Sumar(){
        puntaje += 1;
        texto.text = "Puntaje: " + puntaje.ToString();
        sonido.Play();
    }
}
