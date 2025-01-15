using System.Collections;
using UnityEngine;
using TMPro;
using MixedReality.Toolkit.UX;

/**
 * Clase que maneja el juego de la línea recta.
 * Esta clase se encarga de activar y desactivar el juego, mostrar mensajes emergentes,
 * iniciar el juego en sí, y manejar el fin del juego.
 */
public class JuegoLinea : MonoBehaviour{
	public bool activo = false, fin = true;
	public GameObject animal1, animal2, animal3, animal4, interfazMenu, interfazJuego,
					  flecha, centro, boton;
	public TextMeshPro mensaje, historia;
	public Transform camara;
	private float[] posx = {0.6f, 2.0f, -0.6f, -2.0f}, posz = {2.5f, 2.0f, 2.5f, 2.0f};
	private AudioSource sonido;

	/**
	 * Inicializa el sonido del juego.
	 */
	public void Awake(){
		sonido = GetComponent<AudioSource>();
	}

    /**
     * Inicializa la interfaz del menú y el mensaje de historia.
     */
    void Start(){
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
		historia.GetComponent<MensajeEmergente>().CambiarTexto("Presiona el botón grande para iniciar");
    }

    /**
     * Actualiza el estado del juego y maneja los eventos de inicio y fin.
     */
    void Update(){
		PressableButton boton1;
		if(activo && fin){
			Activar();
			fin = false;
			Activar();
			historia.GetComponent<MensajeEmergente>().CambiarTexto("");
			StartCoroutine(Juego2());
			historia.GetComponent<MensajeEmergente>().CambiarTexto("El mono tiene hambre, vamos a alimentarlo!\nPresiona el botón grande para continuar");
			boton1 = boton.GetComponent<PressableButton>();
			boton1.OnClicked.RemoveAllListeners();
			boton1.OnClicked.AddListener(() => boton1.GetComponent<ControladorEscena>().CambiarEscena("escenaFruta"));
		}
    }

	/**
	 * Corrutina que maneja el juego de la línea recta.
	 */
	IEnumerator Juego2(){
		int i, j, id, contador;
		bool siguiente;
		float dif;
		Vector3 xx, zz, dirC, dirA;
		Vector3[] posiciones = new Vector3[4];
		GameObject[] animalesI = {animal1, animal2, animal3, animal4},
					 animales = new GameObject[4];
		centro.transform.position = camara.position;
		interfazMenu.SetActive(false); interfazJuego.SetActive(true);
		// Cuenta la historia.
		mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Vamos a conocer a los amigos del mono!\nCamina en línea recta hacia el animal que haga sonido!");
		yield return new WaitForSeconds(10);
		mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Puedes mirar hacia los lados si no encuentras al animal");
		yield return new WaitForSeconds(10);
		mensaje.GetComponent<MensajeEmergente>().CambiarTexto("");
		// Ciclo que realiza el juego para cada animal.
		for(i = 0; i < 4; i++){
			contador = 0; siguiente = true; dif = 0; id = i;
			mensaje.GetComponent<MensajeEmergente>().CambiarTexto("");
			xx = camara.right; zz = camara.forward;
			xx[1] = 0.0f; zz[1] = 0.0f;
			// Inicializa los animales.
			for(j = 0; j < 4; j++){
				posiciones[j] = camara.position + (posx[j] * xx.normalized) + (posz[j] * zz.normalized);
				posiciones[j][1] = 0.0f;
				animales[j] = Instantiate(animalesI[j], posiciones[j], Quaternion.Euler(0, camara.transform.eulerAngles.y, 0));
			}
			animales[id].GetComponent<ComportamientoAnimal>().ActivarTurno();
			// Espera hasta el usuario llegue al animal. Emite su sonido cada 15 segundos.
			while(siguiente){
				if(contador % 15 == 0){
					animales[id].GetComponent<ComportamientoAnimal>().EmitirSonido();
				}
				yield return new WaitForSeconds(1);
				mensaje.GetComponent<MensajeEmergente>().CambiarTexto("");
				for(j = 0; j < 4; j++){
					if(animales[j].GetComponent<ComportamientoAnimal>().cerca){
						if(animales[j].GetComponent<ComportamientoAnimal>().turno){
							siguiente = false;
							mensaje.GetComponent<MensajeEmergente>().CambiarTexto(animales[j].GetComponent<ComportamientoAnimal>().lineaRecta ? "Muy bien!" : "Intenta caminar más derecho");
							if(animales[j].GetComponent<ComportamientoAnimal>().lineaRecta){
								sonido.Play();
								SistemaPuntos.instancia.Sumar();
							}
						}else{
							mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Ese no es :(");
						}
					}
				}
				contador++;
			}
			// Elimina a los animales.
			for(j = 0; j < 4; j++){
				Destroy(animales[j]);
			}
			// Espera hasta que el usuario esté mirando al centro del juego para continuar.
			yield return new WaitForSeconds(3);
			mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Mira hacia donde apunta la flecha");
			flecha.SetActive(true);
			while(dif < 0.90){
				dirC = (centro.transform.position - camara.position).normalized;
				dirA = camara.forward.normalized;
				dirC[1] = 0; dirA[1] = 0;
				dirC.Normalize(); dirA.Normalize();
				dif = Vector3.Dot(dirA, dirC);
				yield return new WaitForSeconds(0.1f);
			}
			flecha.SetActive(false);
		}
		// Termina el juego.
		yield return new WaitForSeconds(1);
		mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Se acabó el juego!");
		yield return new WaitForSeconds(3);
		SistemaPuntos.instancia.Reiniciar();
		interfazMenu.SetActive(true); interfazJuego.SetActive(false);
	}

	/**
     * Cambia el estado del juego.
     */
	public void Activar(){
		activo = !activo;
	}
}
