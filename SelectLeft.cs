using UnityEngine;
using System.Collections;

public class SelectLeft : MonoBehaviour
{
    // アニメーションが終わったかどうかの判定
    private bool Animed = false;
    float animTime = 0;
    // Shotの読み込み
    Shot shotScript;
    // bulletの配列の中身を指定する
    private int bulletNum = 1;
    public int _bulletNum { get { return bulletNum; } }
    // bulletListの読み込み
    BulletList bulletList;
    // public GameObject bulletKindLeft;
    private Animator bookAnim;

    // Use this for initialization
    void Start()
    {
        bulletList = GameObject.Find("BulletList").GetComponent<BulletList>();

        // 右手の弾の初期値
        // bulletKindLeft = bulletList._bulletList[0].normalBullet[bulletNum];
        bookAnim = GameObject.Find("BookLeft").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 本がアニメーションしている場合、カウントを進める
        if (Animed == true)
        {
            animTime += Time.deltaTime;
            if (animTime >= 5)
            {
                animTime = 0;
                Animed = false;
            }
        }
    }

    // 本のcolliderに入ったら、Shotスクリプトを無効化
    void OnTriggerEnter(Collider other)
    {
        shotScript = GameObject.Find("ShotObject").GetComponent<Shot>();
        if (other.CompareTag("Center") || other.CompareTag("Left_right") || other.CompareTag("Left_left"))
        {
            shotScript.enabled = false;
            Destroy(shotScript.bulletLeftClone);
            Destroy(shotScript.bulletRightClone);
        }
    }

    // 魔法の切り替え
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Left_right"))
        {
            if (Animed == false)
            {
                if (common.leftMagicNum == 4)
                {
                    common.leftMagicNum = 0;
                }
                Animed = true;
                bookAnim.SetTrigger("Back_Tri");
                common.leftMagicNum++;
                common.bulletKindLeft = bulletList._bulletList[0].normalBullet[common.leftMagicNum];
            }
        }
        if (other.CompareTag("Left_left"))
        {
            if (Animed == false)
            {
                if (common.leftMagicNum == 1)
                {
                    common.leftMagicNum = 5;
                }
                Animed = true;
                bookAnim.SetTrigger("Go_Tri");
                common.leftMagicNum--;
                common.bulletKindLeft = bulletList._bulletList[0].normalBullet[common.leftMagicNum];
            }
        }
    }

    // Shotの無効化を解除
    void OnTriggerExit(Collider other)
    {
        // Debug.Log("nuketa!");
        shotScript.enabled = true;
    }
}