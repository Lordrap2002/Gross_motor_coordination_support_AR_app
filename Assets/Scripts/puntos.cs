using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puntos : MonoBehaviour
{
	public TextMeshPro texto;
	int puntaje = 0;
	public static puntos instancia;

	public void Awake(){
		instancia = this;
	}
    // Start is called before the first frame update
    void Start()
    {
        texto.text = "Puntaje: " + puntaje.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void sumar(){
		puntaje += 1;
		texto.text = "Puntaje: " + puntaje.ToString();
	}
}
