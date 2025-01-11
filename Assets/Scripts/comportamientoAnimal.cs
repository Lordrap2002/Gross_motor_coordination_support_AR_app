using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Clase que maneja el comportamiento de un animal en el juego.
 * Esta clase se encarga de controlar la rotación del animal hacia la cámara,
 * detectar si la cámara está cerca del animal, emitir sonidos y activar el turno del animal.
 */
public class ComportamientoAnimal : MonoBehaviour{
	public Transform camara;
	public bool turno = false;
	public bool cerca = false;
	public bool lineaRecta = true;
	public float desvio;
	private AudioSource sonido;
	private Vector3 zona;
	private Vector3 direccionI;
	private Vector3 direccionA;

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
		direccionI = transform.position - camara.position;
		direccionI[1] = 0.0f;
    }

    /**
     * Actualiza la rotación del animal hacia la cámara, detecta si la cámara está cerca,
     * y ajusta el desvío y la línea recta según sea necesario.
     */
    void Update(){
		Vector3 direccion = camara.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 180f + Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg, 0);   
		if(turno){
			direccionA = transform.position - camara.position;
			direccionA[1] = 0.0f;
			desvio = Vector3.Dot(direccionI.normalized, direccionA.normalized);
			if(desvio < 0.95){
				lineaRecta = false;
			}
		}
		zona = transform.position + ((camara.position - transform.position).normalized * 1.25f);
		if(camara.position[0] <= (zona[0] + 0.2f) && camara.position[0] >= (zona[0] - 0.2f)
		&& camara.position[2] <= (zona[2] + 0.2f) && camara.position[2] >= (zona[2] - 0.2f)){
			this.cerca = true;
		}else{
			this.cerca = false;
		}
	}

	/**
	 * Emite un sonido.
	 */
	public void emitirSonido(){
		sonido.Play();
    }

	/**
	 * Activa el turno del animal.
	 */
	public void activarTurno(){
		this.turno = true;
    }
}
