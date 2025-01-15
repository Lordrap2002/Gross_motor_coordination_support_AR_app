using System.Collections;
using UnityEngine;
using TMPro;
using MixedReality.Toolkit.UX;

/**
 * Clase que maneja el juego de imitar los movimientos del animal.
 * Esta clase se encarga de activar y desactivar el juego, mostrar mensajes emergentes,
 * iniciar el juego en sí, y manejar el fin del juego.
 */
public class JuegoImitar : MonoBehaviour{
	public bool activo = false, fin = true;
	public GameObject animal, interfazMenu, interfazJuego, boton;
	public TextMeshPro mensaje, historia;
	public Transform camara;
	public Animator animator;

    /**
     * Inicializa la interfaz del menú y el mensaje de historia.
     */
    void Start(){
        interfazMenu.SetActive(true); interfazJuego.SetActive(false);
        historia.GetComponent<MensajeEmergente>().CambiarTexto("Presiona el botón grande para iniciar");
        animator = animal.GetComponent<Animator>();
    }

    /**
     * Actualiza el estado del juego y maneja los eventos de inicio y fin.
     */
    void Update(){
        PressableButton boton1;
        if(activo && fin){
            Activar();
            fin = false;
            historia.GetComponent<MensajeEmergente>().CambiarTexto("");
            StartCoroutine(Juego1());
            historia.GetComponent<MensajeEmergente>().CambiarTexto("El mono quiere presentarte a sus amigos, vamos a conocerlos!\nPresiona el botón grande para continuar");
            boton1 = boton.GetComponent<PressableButton>();
            boton1.OnClicked.RemoveAllListeners();
            boton1.OnClicked.AddListener(() => boton1.GetComponent<ControladorEscena>().CambiarEscena("escenaLinea"));
        }
    }

    /**
     * Corrutina que maneja el juego de imitar los movimientos del animal.
     */
    IEnumerator Juego1(){
        // Inicializa al animal a cierta distancia al frente del usuario y mirando al usuario.
        interfazMenu.SetActive(false); interfazJuego.SetActive(true); animal.SetActive(true);
        Vector3 posicionObj = camara.position + new Vector3(camara.forward.x, 0, camara.forward.z).normalized * 2.0f;
        animal.transform.position = new Vector3(posicionObj.x, animal.transform.position.y, posicionObj.z);
        Vector3 direccion = camara.position - animal.transform.position;
        animal.transform.rotation = Quaternion.Euler(0, 180f + Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg, 0);
        posicionObj = new Vector3(camara.position.x, animal.transform.position.y, camara.position.z);
        animal.transform.LookAt(posicionObj);
        // Cuenta la historia.
        mensaje.GetComponent<MensajeEmergente>().CambiarTexto("El mono quiere ser tu amigo!\nImita sus movimientos para ganar su confianza!");
        yield return new WaitForSeconds(10);
        mensaje.GetComponent<MensajeEmergente>().CambiarTexto("");
        // Inicia la animación del animal.
        animator.Play("EjercicioCompleto");
        yield return new WaitForSeconds(60);
        // Termina el juego.
        mensaje.GetComponent<MensajeEmergente>().CambiarTexto("Se acabó el juego!");
        yield return new WaitForSeconds(3);
        interfazMenu.SetActive(true); interfazJuego.SetActive(false); animal.SetActive(false);
    }

    /**
     * Cambia el estado del juego.
     */
    public void Activar(){
        activo = !activo;
    }
}
