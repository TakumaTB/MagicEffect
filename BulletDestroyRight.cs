using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletDestroyRight : MonoBehaviour
{
    public float destroyTime = 5.0f;
    private Shot _Shot;

    // Use this for initialization
    void Start ()
    {
        _Shot = GameObject.Find("ShotObject").GetComponent<Shot>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 右手で魔法を撃ったら、destroyTimeの時間後に削除
        if(common.bulletFireRight == true)
        {
            Destroy(_Shot.bulletRightClone, destroyTime);
        }
    }
}
