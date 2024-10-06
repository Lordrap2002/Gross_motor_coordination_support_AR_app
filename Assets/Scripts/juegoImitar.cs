using System.Collections;
using UnityEngine;
using TMPro;
using MixedReality.Toolkit.UX;

public class JuegoImitar : MonoBehaviour{
	public bool activo = false, fin = true;
	public GameObject animal1, interfazMenu, interfazJuego, boton;
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
			historia.GetComponent<MensajeEmergente>().cambiarTexto("");
			StartCoroutine(Juego1());
			historia.GetComponent<MensajeEmergente>().cambiarTexto("Parece que el mono quiere presentarte a sus amigos, vamos a conocerlos!");
			boton1 = boton.GetComponent<PressableButton>();
			boton1.OnClicked.RemoveAllListeners();
			boton1.OnClicked.AddListener(() => boton1.GetComponent<SceneLoader>().LoadScene("escena2"));
		}
    }

	IEnumerator Juego1(){
		int i;
		interfazMenu.SetActive(false); interfazJuego.SetActive(true); animal1.SetActive(true);
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Imita los movimientos del mono!");
		yield return new WaitForSeconds(5);
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("");
		for(i = 0; i < 1; i++){
			animal1.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, 1.5f);
			yield return new WaitForSeconds(5);
		}
		mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Se acabó el juego!");
		yield return new WaitForSeconds(3);
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
	}

	public void Activar(){
		activo = !activo;
	}
}
