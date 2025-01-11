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
	public GameObject animal1, interfazMenu, interfazJuego, boton;
	public TextMeshPro mensaje, historia;
	public Transform camara;
	public Animator animator;

    /**
     * Inicializa la interfaz del menú y el mensaje de historia.
     */
    void Start(){
        interfazMenu.SetActive(true); interfazJuego.SetActive(false);
        historia.GetComponent<MensajeEmergente>().cambiarTexto("Oprime el botón grande para iniciar");
        animator = animal1.GetComponent<Animator>();
    }

    /**
     * Actualiza el estado del juego y maneja los eventos de inicio y fin.
     */
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

    /**
     * Corrutina que maneja el juego de imitar los movimientos del animal.
     */
    IEnumerator Juego1(){
        interfazMenu.SetActive(false); interfazJuego.SetActive(true); animal1.SetActive(true);
        Vector3 targetPosition = camara.position + new Vector3(camara.forward.x, 0, camara.forward.z).normalized * 2.0f;
        animal1.transform.position = new Vector3(targetPosition.x, animal1.transform.position.y, targetPosition.z);
        Vector3 direccion = camara.position - animal1.transform.position;
        animal1.transform.rotation = Quaternion.Euler(0, 180f + Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg, 0);
        Vector3 targetPos = new Vector3(camara.position.x, animal1.transform.position.y, camara.position.z);
        animal1.transform.LookAt(targetPos);
        mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Imita los movimientos del mono!");
        yield return new WaitForSeconds(5);
        mensaje.GetComponent<MensajeEmergente>().cambiarTexto("");
        animator.Play("EjercicioCompleto");
        yield return new WaitForSeconds(60);
        mensaje.GetComponent<MensajeEmergente>().cambiarTexto("Se acabó el juego!");
        yield return new WaitForSeconds(3);
        interfazMenu.SetActive(true); interfazJuego.SetActive(false); animal1.SetActive(false);
    }

    /**
     * Cambia el estado del juego.
     */
    public void Activar(){
        activo = !activo;
    }
}
