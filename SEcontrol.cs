using UnityEngine;
using System.Collections;

public class SEcontrol : MonoBehaviour
{
    private AudioSource SEsource;
    public AudioClip keepSE;
    public AudioClip breakSE;
    public GameObject SoundObj;
    private GameObject SoundObjClone;

    // Use this for initialization
    void Start ()
    {
        // 魔法が手にあるときの効果音
        SEsource = this.GetComponent<AudioSource>();
        SEsource.clip = keepSE;
        SEsource.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    // 魔法が当たった場所に効果音を発生させるオブジェクトを生成
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "target")
        {
            SoundObjClone = Instantiate(SoundObj, this.transform.position, Quaternion.identity) as GameObject;
            SoundObjClone.GetComponent<AudioSource>().clip = breakSE;
            SoundObjClone.GetComponent<AudioSource>().Play();
            Destroy(SoundObjClone, 2.0f);
        }
    }
}
