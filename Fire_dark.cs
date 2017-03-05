using UnityEngine;
using System.Collections;

public class Fire_dark : MonoBehaviour
{
    public GameObject effect;
    private GameObject effectClone;
    private int count = 0;

    float time = 0;

    Detonator _Detonator;
    Test _Test;

    // Use this for initialization
    void Start ()
    {
        _Detonator = GetComponent<Detonator>();
        effectClone = GameObject.Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
        effectClone.SetActive(false);
        // _Test = GameObject.Find("Player").GetComponent<Test>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if(common.bulletFireRight == false)
        {
            count = 0;
        }
        */
	}

    void OnCollisionEnter(Collision other)
    {
        /*
        count++;
        if(count == 1)
        {

            if (other.gameObject.tag == "Enemy")
            {
                _Detonator.Explode();
                effectClone.transform.position = this.transform.position;
                effectClone.SetActive(true);

                Destroy(gameObject, 2f);
            }
        }
        */
        if (other.gameObject.tag == "target")
        {
            _Detonator.Explode();
            effectClone.transform.position = this.transform.position;
            effectClone.SetActive(true);

            Destroy(gameObject);
        }
    }
}
