using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportamiento : MonoBehaviour
{
	public GameObject fruta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fruta.transform.Rotate(Vector3.down, 0.2f);
    }
	private void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "piso"){
			Destroy(this.gameObject);
		}
	}

	public void tocar(){
		Destroy(this.gameObject);
		puntos.instancia.sumar();
	}
}
