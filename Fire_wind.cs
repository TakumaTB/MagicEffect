using UnityEngine;
using System.Collections;

public class Fire_wind : MonoBehaviour
{
    Detonator _detonator;

    public float coefficient;   // 空気抵抗係数
    public float speed;         // 爆風の速さ

    public float duration = 5f;
    public GameObject blast;
    private GameObject blastClone;

    // Use this for initialization
    void Start ()
    {
        _detonator = GetComponent<Detonator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            _detonator.Explode();
            Rigidbody air = other.GetComponent<Rigidbody>();
            if (other.tag == "target")
            {
                if (air == null)
                {
                    return;
                }

                // 風速計算
                Vector3 velocity = (other.transform.position - transform.position).normalized * speed;

                velocity.y += 100f;

                // 風力与える
                air.AddForce(coefficient * velocity);
            }
            blastClone = GameObject.Instantiate(blast, transform.position, Quaternion.identity)as GameObject;

            Destroy(this, duration);
        }
    }
}
