using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juego : MonoBehaviour
{
	bool activo = false;
	public GameObject fruta;
	public TextMeshPro mensaje;

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
		if(activo){
			activo = false;
			StartCoroutine(Juego3());
		}
    }

	IEnumerator Juego3(){
		MensajeEmergente.instancia.cambiarTexto("Atrapa las frutas antes de que se acabe el tiempo!");
		yield return new WaitForSeconds(3);
		MensajeEmergente.instancia.cambiarTexto("");
		int tiempoMax = Temporizador.instancia.tiempo;
		for(int i = 0; i < tiempoMax; i++){
			yield return new WaitForSeconds(1);
			Temporizador.instancia.restarSegundo();
			if((i % 3) == 0){
				Vector3 pos = new Vector3(Random.Range(-0.4f, 0.4f), 2.0f, Random.Range(0.5f, 1.0f));
				Instantiate(fruta, pos, Quaternion.identity);
			}
		}
		yield return new WaitForSeconds(1);
		MensajeEmergente.instancia.cambiarTexto("Se acabó el tiempo!");
	}

	public void activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
