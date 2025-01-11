using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Clase que se encarga de cerrar el juego.
 * Esta clase proporciona un método para cerrar el juego, tanto en el editor como en la versión compilada.
 */
public class Salir : MonoBehaviour{
    /**
     * Cierra el juego.
     * Este método cierra el juego, si se está ejecutando en el editor, también detiene la ejecución en el editor.
     */
    public void CerrarJuego(){
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
