using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionController : MonoBehaviour{
	public Transform camara;
    public float xpos, ypos, zpos;

    void Start(){
        transform.position = camara.position + (camara.forward * zpos)
							+ (camara.right * xpos) + (camara.up * ypos);
		transform.rotation = camara.rotation;
    }

    void Update(){
        transform.position = camara.position + (camara.forward * zpos)
							+ (camara.right * xpos) + (camara.up * ypos);
		transform.rotation = camara.rotation;
    }
}
