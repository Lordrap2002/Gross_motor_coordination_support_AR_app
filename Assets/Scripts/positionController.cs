using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionController : MonoBehaviour
{
	public Transform camara;
    public float xpos, ypos, zpos;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 resultingPosition = camara.position + (camara.forward * zpos)
									+ (camara.right * xpos)
									+ (camara.up * ypos);
        Quaternion resultingRotation = camara.rotation;
        transform.position = resultingPosition;
		transform.rotation = resultingRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resultingPosition = camara.position + (camara.forward * zpos)
									+ (camara.right * xpos)
									+ (camara.up * ypos);
        Quaternion resultingRotation = camara.rotation;
        transform.position = resultingPosition;
		transform.rotation = resultingRotation;
    }
	
}
