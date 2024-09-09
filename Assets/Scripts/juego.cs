using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juego : MonoBehaviour
{
	bool activo = false;
	public GameObject fruta1, fruta2, fruta3, interfazMenu, interfazJuego;
	public TextMeshPro mensaje;
	public Transform camara;

    // Start is called before the first frame update
    void Start(){
		interfazMenu.SetActive(true);
		interfazJuego.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
		if(activo){
			activo = false;
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
		GameObject[] frutas = new GameObject[3];
		frutas[0] = fruta1;
		frutas[1] = fruta2;
		frutas[2] = fruta3;
		for(int i = 0; i < tiempoMax; i++){
			yield return new WaitForSeconds(1);
			Temporizador.instancia.restarSegundo();
			if((i % 3) == 0){
				Vector3 pos = new Vector3(Random.Range(-0.4f, 0.4f), 0.4f, Random.Range(0.5f, 1.0f)) + camara.position;
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

	public void activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
