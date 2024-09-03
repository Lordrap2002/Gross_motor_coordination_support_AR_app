using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
	public static Temporizador instancia;
	public TextMeshPro texto;
	public int tiempo = 30;

	public void Awake(){
		instancia = this;
	}
    // Start is called before the first frame update
    void Start()
    {
        texto.text = "Tiempo: " + tiempo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void restarSegundo(){
		tiempo -= (tiempo > 0 ? 1 : 0);
		texto.text = "Tiempo: " + tiempo.ToString();
	}
}
