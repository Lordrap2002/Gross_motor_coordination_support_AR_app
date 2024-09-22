using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntos : MonoBehaviour{
	public static Puntos instancia;
	public TextMeshPro texto;
	public int puntaje = 0;
	private AudioSource sonido;

	public void Awake(){
		instancia = this;
		sonido = GetComponent<AudioSource>();
	}

    void Start(){
        texto.text = "Puntaje: " + puntaje.ToString();
    }

    void Update(){
        
    }

	public void reiniciar(){
		puntaje = 0;
		texto.text = "Puntaje: " + puntaje.ToString();
	}

	public void sumar(){
		puntaje += 1;
		texto.text = "Puntaje: " + puntaje.ToString();
		sonido.Play();
	}
}
