using System.Collections;
using UnityEngine;
using TMPro;
using MixedReality.Toolkit.UX;
using Unity.VisualScripting;

public class JuegoFruta : MonoBehaviour{
	bool activo = false, fin = true;
	public GameObject fruta1, fruta2, fruta3, interfazMenu, interfazJuego, boton;
	public TextMeshPro mensaje, historia;
	public Transform camara;

    void Start(){
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
		historia.GetComponent<MensajeEmergente>().cambiarTexto("Oprime el botón grande para iniciar");
    }

    void Update(){
		PressableButton boton1;
		if(activo && fin){
			Activar();
			fin = false;
			Activar();
			historia.GetComponent<MensajeEmergente>().cambiarTexto("");
			StartCoroutine(Juego3());
			historia.GetComponent<MensajeEmergente>().cambiarTexto("Oprime el boton para salir");
			boton1 = boton.GetComponent<PressableButton>();
			boton1.OnClicked.RemoveAllListeners();
			boton1.OnClicked.AddListener(() => boton1.GetComponent<Salir>().CerrarJuego());
		}
    }

	IEnumerator Juego3(){
		interfazMenu.SetActive(false);
		interfazJuego.SetActive(true);
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Atrapa las frutas antes de que se acabe el tiempo!");
		yield return new WaitForSeconds(3);
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("");
		int tiempoMax = Temporizador.instancia.tiempoMax;
		GameObject[] frutas = {fruta1, fruta2, fruta3};
		for(int i = 0; i < tiempoMax; i++){
			yield return new WaitForSeconds(1);
			Temporizador.instancia.restarSegundo();
			if((i % 3) == 0){
				mensaje.GetComponent<MensajeEmergente>().cambiarTexto("");
				Vector3 pos = new Vector3(Random.Range(-0.2f, 0.2f), 0.2f, 0.0f) + camara.position + (new Vector3(camara.forward.x, 0, camara.forward.z).normalized * Random.Range(0.5f, 0.8f));
				Instantiate(frutas[Random.Range(0,3)], pos, Quaternion.identity);
			}
		}
		yield return new WaitForSeconds(1);
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Se acabó el tiempo!");
		yield return new WaitForSeconds(3);
		Temporizador.instancia.reiniciar();
		Puntos.instancia.reiniciar();
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
	}

	public void Activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
