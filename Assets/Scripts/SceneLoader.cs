using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Clase que se encarga de cargar escenas en el juego.
 * Esta clase proporciona un método para cargar una escena específica por su nombre.
 */
public class SceneLoader : MonoBehaviour{
    /**
     * Carga una escena específica por su nombre.
     */
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
