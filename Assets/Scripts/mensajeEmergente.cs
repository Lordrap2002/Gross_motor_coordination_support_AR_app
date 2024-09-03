using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MensajeEmergente : MonoBehaviour
{
	public static MensajeEmergente instancia;
	public TextMeshPro texto;

	public void Awake(){
		instancia = this;
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void cambiarTexto(string mensaje){
		texto.text = mensaje;
	}

}
