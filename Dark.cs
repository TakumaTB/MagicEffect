using UnityEngine;
using System.Collections;

public class Dark : MonoBehaviour
{
    public ParticleSystem darkEffect;
    private ParticleSystem darkEffectClone;

	// Use this for initialization
	void Start ()
    {
        	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            other.gameObject.AddComponent<Delete>();
            darkEffectClone = Instantiate(darkEffect, other.transform.position, Quaternion.identity) as ParticleSystem;
            Destroy(this.gameObject);
        }
    }
}
