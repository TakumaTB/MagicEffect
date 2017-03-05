using UnityEngine;
using System.Collections;

public class T_explosion : MonoBehaviour
{
    public float explosionPower = 500.0f;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, transform.position, 50);
        }
    }
}
