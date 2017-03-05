using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectRight : MonoBehaviour
{
    private Shot shotScript;
    /*
    //-----------------テスト用-----------------//
    public GameObject ball;
    private Renderer ballRenderer;
    public Material[] materialArray = new Material[3];
    //------------------------------------------//
    */

    private bool Animed = false;
    private float animTime = 0;
  
    BulletList bulletList;
    // public GameObject bulletKindRight;

    private int bulletNum = 1;
    public int _bulletNum { get { return bulletNum; } }

    private Animator bookAnim;

	// Use this for initialization
	void Start ()
    {
        // ballRenderer = ball.GetComponent<Renderer>();
        bulletList = GameObject.Find("BulletList").GetComponent<BulletList>();

        // 右手の弾の初期値
        // bulletKindRight = bulletList._bulletList[0].normalBullet[bulletNum];

        bookAnim = GameObject.Find("BookRight").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Animed == true)
        {
            animTime += Time.deltaTime;
            if(animTime >= 3f)
            {
                animTime = 0;
                Animed = false;
            }
        }
        Debug.Log(common.rightMagicNum);
    }

    void OnTriggerEnter(Collider other)
    {
        shotScript = GameObject.Find("ShotObject").GetComponent<Shot>();
        if (other.gameObject.tag == "Right_right" || other.gameObject.tag == "Right_left" || other.gameObject.tag == "Center")
        {
            shotScript.enabled = false;
            Destroy(shotScript.bulletLeftClone);
            Destroy(shotScript.bulletRightClone);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Right_left"))
        {
            if (Animed == false)
            {
                if (common.rightMagicNum == 4)
                {
                    common.rightMagicNum = 0;
                }
                Animed = true;
                bookAnim.SetTrigger("Go_Tri");
                common.rightMagicNum++;
                common.bulletKindRight = bulletList._bulletList[0].normalBullet[common.rightMagicNum];
            }
        }
        if (other.CompareTag("Right_right"))
        {
            if (Animed == false)
            {
                if (common.rightMagicNum == 1)
                {
                    common.rightMagicNum = 5;
                }
                Animed = true;
                bookAnim.SetTrigger("Back_Tri");
                common.rightMagicNum--;
                common.bulletKindRight = bulletList._bulletList[0].normalBullet[common.rightMagicNum];
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        shotScript.enabled = true;
    }
}