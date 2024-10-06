using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salir : MonoBehaviour{
    public void CerrarJuego(){
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

