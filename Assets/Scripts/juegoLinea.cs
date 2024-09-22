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
		float desvio;
		bool siguiente, lineaRecta;
		Vector3[] posiciones = new Vector3[4];
		Vector3 xx, zz, zona, direccionI, direccionA;
		GameObject[] animalesI = {animal1, animal2, animal3, animal4},
					 animales = new GameObject[4];
		interfazMenu.SetActive(false); interfazJuego.SetActive(true);
		MensajeEmergente.instancia.cambiarTexto("Camina hacia el animal que haga sonido!");
		yield return new WaitForSeconds(3);
		MensajeEmergente.instancia.cambiarTexto("");
		//int tiempoMax = Temporizador.instancia.tiempoMax;
		for(i = 0; i < 4; i++){
			cont = 0; siguiente = true; lineaRecta = true;
			yield return new WaitForSeconds(1);
			MensajeEmergente.instancia.cambiarTexto("");
			//Temporizador.instancia.restarSegundo();
			xx = camara.right; zz = camara.forward;
			xx[1] = 0.0f; zz[1] = 0.0f;
			for(j = 0; j < 4; j++){
				posiciones[j] = camara.position + (posx[j] * xx.normalized) + (posz[j] * zz.normalized);
				posiciones[j][1] = 0.0f;
				animales[j] = Instantiate(animalesI[j], posiciones[j], Quaternion.Euler(0, camara.transform.eulerAngles.y, 0));
			}
			id = UnityEngine.Random.Range(0,4);
			zona = posiciones[id] + ((camara.position - posiciones[id]).normalized * 0.75f);
			direccionI = posiciones[id] - camara.position;
			direccionI[1] = 0.0f;
			while(siguiente){
				if(cont % 15 == 0){
					animales[id].GetComponent<ComportamientoAnimal>().emitirSonido();
				}
				direccionA = posiciones[id] - camara.position;
				direccionA[1] = 0.0f;
				desvio = Vector3.Dot(direccionI.normalized, direccionA.normalized);
				if(desvio < 0.99){
					lineaRecta = false;
					Debug.Log("El usuario se ha desviado de la línea recta.");
				}
				if(camara.position[0] <= (zona[0] + 0.2f) && camara.position[0] >= (zona[0] - 0.2f)
				&& camara.position[2] <= (zona[2] + 0.2f) && camara.position[2] >= (zona[2] - 0.2f)){
					Destroy(animales[id]);
					siguiente = false;
					MensajeEmergente.instancia.cambiarTexto(lineaRecta ? "Muy bien!" : "Oh no");
				}
				yield return new WaitForSeconds(1);
				cont++;
			}
			for(j = 0; j < 4; j++){
				if(j != id){
					Destroy(animales[j]);
				}
			}
		}
		yield return new WaitForSeconds(1);
		MensajeEmergente.instancia.cambiarTexto("Se acabó el juego!");
		yield return new WaitForSeconds(3);
		//Temporizador.instancia.reiniciar();
		//Puntos.instancia.reiniciar();
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
	}

	public void Activar(){
		activo = !activo;
	}
}
