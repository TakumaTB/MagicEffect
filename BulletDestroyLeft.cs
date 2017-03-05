using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletDestroyLeft : MonoBehaviour
{
    public float destroyTime = 5.0f;
    private Shot _Shot;

    // Use this for initialization
    void Start()
    {
        _Shot = GameObject.Find("ShotObject").GetComponent<Shot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (common.bulletFireLeft == true)
        {
            Destroy(_Shot.bulletLeftClone, destroyTime);
        }
    }
}
