using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(juego3());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	bool activo = false;
	public GameObject fruta;
	IEnumerator juego3(){
		for(int i = 0; i < 30; i++){
			yield return new WaitForSeconds(4);
			if(activo){
				Vector3 pos = new Vector3(Random.Range(-0.2f, 0.2f), 1.8f, 0.5f);
				Instantiate(fruta, pos, Quaternion.identity);
			}
		}
	}

	public void activar(){
		if(activo){
			activo = false;
		}else{
			activo = true;
		}
	}
}
