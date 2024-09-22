using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuegoFruta : MonoBehaviour{
	bool activo = false;
	public GameObject fruta1, fruta2, fruta3, interfazMenu, interfazJuego;
	public TextMeshPro mensaje;
	public Transform camara;

    void Start(){
		interfazMenu.SetActive(true);
		interfazJuego.SetActive(false);
    }

    void Update(){
		if(activo){
			Activar();
			StartCoroutine(Juego3());
		}
    }

	IEnumerator Juego3(){
		interfazMenu.SetActive(false);
		interfazJuego.SetActive(true);
		MensajeEmergente.instancia.cambiarTexto("Atrapa las frutas antes de que se acabe el tiempo!");
		yield return new WaitForSeconds(3);
		MensajeEmergente.instancia.cambiarTexto("");
		int tiempoMax = Temporizador.instancia.tiempoMax;
		GameObject[] frutas = {fruta1, fruta2, fruta3};
		for(int i = 0; i < tiempoMax; i++){
			yield return new WaitForSeconds(1);
			Temporizador.instancia.restarSegundo();
			if((i % 3) == 0){
				MensajeEmergente.instancia.cambiarTexto("");
				Vector3 pos = new Vector3(Random.Range(-0.15f, 0.15f), 0.2f, Random.Range(0.5f, 0.7f)) + camara.position;
				Instantiate(frutas[Random.Range(0,3)], pos, Quaternion.identity);
			}
		}
		yield return new WaitForSeconds(1);
		MensajeEmergente.instancia.cambiarTexto("Se acabó el tiempo!");
		yield return new WaitForSeconds(3);
		Temporizador.instancia.reiniciar();
		Puntos.instancia.reiniciar();
		interfazMenu.SetActive(true);
		interfazJuego.SetActive(false);
	}

	public void Activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
