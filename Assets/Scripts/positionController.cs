using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Clase que controla la posición de un objeto en el espacio.
 * Esta clase se encarga de posicionar un objeto en relación a una cámara, 
 * permitiendo ajustar la posición en los ejes x, y, z.
 */
public class positionController : MonoBehaviour{
	public Transform camara;
    public float xpos, ypos, zpos;

    /**
     * Inicializa la posición del objeto en el espacio.
     * Establece la posición del objeto basándose en la posición de la cámara y los valores de xpos, ypos, zpos.
     */
    void Start(){
        transform.position = camara.position + (camara.forward * zpos)
							+ (camara.right * xpos) + (camara.up * ypos);
        transform.rotation = camara.rotation;
    }

    /**
     * Actualiza la posición del objeto en cada frame.
     * Continúa ajustando la posición del objeto basándose en la posición de la cámara y los valores de xpos, ypos, zpos.
     */
    void Update(){
        transform.position = camara.position + (camara.forward * zpos)
							+ (camara.right * xpos) + (camara.up * ypos);
        transform.rotation = camara.rotation;
    }
}
