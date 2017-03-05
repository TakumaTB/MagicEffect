using UnityEngine;
using System.Collections;

public class Fire_light : MonoBehaviour
{
    public GameObject blast;
    private GameObject blastClone;
    Detonator _detonator;

    float ColAngle;

    // Use this for initialization
    void Start()
    {
        _detonator = GetComponent<Detonator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            // _detonator.Explode();
            ColAngle = Vector3.Angle(this.transform.position, other.gameObject.transform.position);
            
            blastClone = Instantiate(blast, other.gameObject.transform.position, Quaternion.identity) as GameObject;
        }
    }
    */

    // colliderに入ったら、接触位置に爆風を設置
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "target")
        {
            // _detonator.Explode();
            // ColAngle = Vector3.Angle(this.transform.position, other.gameObject.transform.position);
            foreach(ContactPoint point in other.contacts)
            {
                blastClone = Instantiate(blast, (Vector3)point.point, Quaternion.identity) as GameObject;
            }
        }
    }
}
