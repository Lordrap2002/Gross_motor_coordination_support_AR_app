using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class JuegoLinea : MonoBehaviour{
	public bool activo = false;
	public GameObject animal1, animal2, animal3, animal4, interfazMenu, interfazJuego;
	public TextMeshPro mensaje;
	public Transform camara;
	private float[] posx = {0.6f, 2.0f, -0.6f, -2.0f}, posz = {2.5f, 2.0f, 2.5f, 2.0f};
	private AudioSource sonido;

	public void Awake(){
		sonido = GetComponent<AudioSource>();
	}

    void Start(){
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
    }

    void Update(){
		if(activo){
			Activar();
			StartCoroutine(Juego2());
		}
    }

	IEnumerator Juego2(){
		int i, j, id, cont;
		bool siguiente;
		Vector3 xx, zz;
		Vector3[] posiciones = new Vector3[4];
		GameObject[] animalesI = {animal1, animal2, animal3, animal4},
					 animales = new GameObject[4];
		interfazMenu.SetActive(false); interfazJuego.SetActive(true);
		MensajeEmergente.instancia.cambiarTexto("Camina hacia el animal que haga sonido!");
		yield return new WaitForSeconds(5);
		MensajeEmergente.instancia.cambiarTexto("");
		//int tiempoMax = Temporizador.instancia.tiempoMax;
		for(i = 0; i < 4; i++){
			cont = 0; siguiente = true;
			id = UnityEngine.Random.Range(0, 4);
			MensajeEmergente.instancia.cambiarTexto("");
			//Temporizador.instancia.restarSegundo();
			xx = camara.right; zz = camara.forward;
			xx[1] = 0.0f; zz[1] = 0.0f;
			for(j = 0; j < 4; j++){
				posiciones[j] = camara.position + (posx[j] * xx.normalized) + (posz[j] * zz.normalized);
				posiciones[j][1] = 0.0f;
				animales[j] = Instantiate(animalesI[j], posiciones[j], Quaternion.Euler(0, camara.transform.eulerAngles.y, 0));
			}
			animales[id].GetComponent<ComportamientoAnimal>().activarTurno();
			while(siguiente){
				if(cont % 15 == 0){
					animales[id].GetComponent<ComportamientoAnimal>().emitirSonido();
				}
				yield return new WaitForSeconds(1);
				MensajeEmergente.instancia.cambiarTexto("");
				for(j = 0; j < 4; j++){
					if(animales[j].GetComponent<ComportamientoAnimal>().cerca){
						if(animales[j].GetComponent<ComportamientoAnimal>().turno){
							siguiente = false;
							MensajeEmergente.instancia.cambiarTexto(animales[j].GetComponent<ComportamientoAnimal>().lineaRecta ? "Muy bien!" : "Oh no");
							sonido.Play();
						}else{
							MensajeEmergente.instancia.cambiarTexto("Ese no es :(");
						}
					}
				}
				cont++;
			}
			for(j = 0; j < 4; j++){
				Destroy(animales[j]);
			}
			yield return new WaitForSeconds(3);
		}
		yield return new WaitForSeconds(1);
		MensajeEmergente.instancia.cambiarTexto("Se acab� el juego!");
		yield return new WaitForSeconds(3);
		//Temporizador.instancia.reiniciar();
		//Puntos.instancia.reiniciar();
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
	}

	public void Activar(){
		activo = !activo;
	}
}