using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Clase que maneja el comportamiento de un animal en el juego.
 * Esta clase se encarga de controlar la rotación del animal hacia la cámara,
 * detectar si la cámara está cerca del animal y de emitir el sonido del animal.
 */
public class ComportamientoAnimal : MonoBehaviour{
	public Transform camara;
	public bool turno = false, cerca = false, lineaRecta = true;
	public float desvio;
	private AudioSource sonido;
	private Vector3 posicionObj, direccionIni, direccionAct;

	/**
	 * Inicializa el componente de audio.
	 */
	public void Awake(){
		sonido = GetComponent<AudioSource>();
	}

    /**
     * Inicializa la cámara y la dirección inicial del animal.
     */
    void Start(){
        camara = Camera.main.transform;
		direccionIni = transform.position - camara.position;
		direccionIni[1] = 0.0f;
    }

    /**
     * Actualiza la rotación del animal hacia la cámara, detecta si la cámara está cerca
     * y valida si está caminando en línea recta.
     */
    void Update(){
		// Valida si el usuario está caminando en línea recta.
		Vector3 direccion = camara.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 180f + Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg, 0);   
		if(turno){
			direccionAct = transform.position - camara.position;
			direccionAct[1] = 0.0f;
			desvio = Vector3.Dot(direccionIni.normalized, direccionAct.normalized);
			if(desvio < 0.95){
				lineaRecta = false;
			}
		}
		// Valida si el usuario ya se acercó lo suficiente al animal.
		posicionObj = transform.position + ((camara.position - transform.position).normalized * 1.25f);
		if(camara.position[0] <= (posicionObj[0] + 0.2f) && camara.position[0] >= (posicionObj[0] - 0.2f)
		&& camara.position[2] <= (posicionObj[2] + 0.2f) && camara.position[2] >= (posicionObj[2] - 0.2f)){
			this.cerca = true;
		}else{
			this.cerca = false;
		}
	}

	/**
	 * Emite el sonido del animal.
	 */
	public void EmitirSonido(){
		sonido.Play();
    }

	/**
	 * Activa el turno del animal.
	 */
	public void ActivarTurno(){
		this.turno = true;
    }
}
