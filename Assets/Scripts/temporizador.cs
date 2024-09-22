using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour{
	public static Temporizador instancia;
	public TextMeshPro texto;
	public int tiempoMax = 30;
	public int tiempo;

	public void Awake(){
		instancia = this;
	}

    void Start(){
		tiempo = tiempoMax;
        texto.text = "Tiempo: " + tiempo.ToString();
    }

    void Update(){
       
    }

	public void reiniciar(){
        tiempo = tiempoMax;
    }

	public void restarSegundo(){
		tiempo -= (tiempo > 0 ? 1 : 0);
		texto.text = "Tiempo: " + tiempo.ToString();
	}
}
