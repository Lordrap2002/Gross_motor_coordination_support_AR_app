using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Clase que se encarga de controlar las escenas en el juego.
 * Esta clase proporciona un método para cargar una escena específica por su nombre 
 * y otro para cerrar el juego.
 */
public class ControladorEscena : MonoBehaviour{
    /**
     * Carga una escena específica por su nombre.
     * Este método utiliza el nombre de la escena para cargarla utilizando el SceneManager de Unity.
     */
    public void CambiarEscena(string nombreEscena){
        SceneManager.LoadScene(nombreEscena);
    }

    /**
     * Cierra el juego.
     * Este método cierra el juego, tanto en el editor como en una aplicación compilada.
     */
    public void CerrarJuego(){
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}