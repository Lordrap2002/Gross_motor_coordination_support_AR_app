using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuegoLinea : MonoBehaviour
{
	bool activo = false;
	public GameObject interfazMenu, interfazJuego;
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
			StartCoroutine(Juego2());
		}
    }

	IEnumerator Juego2(){
		interfazMenu.SetActive(false);
		interfazJuego.SetActive(true);
		MensajeEmergente.instancia.cambiarTexto("Camina hacia el animal que haga sonido!");
		yield return new WaitForSeconds(3);
		MensajeEmergente.instancia.cambiarTexto("");
		int tiempoMax = Temporizador.instancia.tiempoMax;
		GameObject[] frutas = new GameObject[3];
		for(int i = 0; i < tiempoMax; i++){
			yield return new WaitForSeconds(1);
			Temporizador.instancia.restarSegundo();
			if((i % 3) == 0){
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

	public void activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
