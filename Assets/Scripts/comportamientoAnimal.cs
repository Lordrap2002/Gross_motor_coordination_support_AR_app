using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComportamientoAnimal : MonoBehaviour{
	public Transform camara;
	public bool turno = false, cerca = false, lineaRecta = true;
	public float desvio;
	private AudioSource sonido;
	private Vector3 zona, direccionI, direccionA;

	public void Awake(){
		sonido = GetComponent<AudioSource>();
	}

    void Start(){
        camara = Camera.main.transform;
		direccionI = transform.position - camara.position;
		direccionI[1] = 0.0f;
    }

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

	public void emitirSonido(){
		sonido.Play();
    }

	public void activarTurno(){
		this.turno = true;
    }
}
