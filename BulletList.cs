using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletList : MonoBehaviour
{
    [System.Serializable]
    public class bulletList
    {
        public List<GameObject> normalBullet = new List<GameObject>(); // 通常魔法
        public List<GameObject> mixBullet = new List<GameObject>(); // 合体魔法
    }
    public List<bulletList> _bulletList = new List<bulletList>();

    public GameObject mixMagic;

    private GameObject rightPalm;
    private GameObject leftPalm;
    private SelectRight _selectRight;
    private SelectLeft _selectLeft;

    private int mixNum;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mixBulletSelect();
    }
    
    // 左右の手の魔法の種類で合成魔法を選択
    private void mixBulletSelect()
    {
        switch (common.rightMagicNum)
        {
            // 火の魔法
            case 1:
                switch (common.leftMagicNum)
                {
                    // 火の魔法
                    case 1:
                        mixMagic = _bulletList[0].mixBullet[0];
                        break;
                    // 闇の魔法
                    case 2:
                        mixMagic = _bulletList[0].mixBullet[1];
                        break;
                    // 風の魔法
                    case 3:
                        mixMagic = _bulletList[0].mixBullet[2];
                        break;
                    // 光の魔法
                    case 4:
                        mixMagic = _bulletList[0].mixBullet[3];
                        break;
                    default:
                        break;
                }
                break;

            // 闇の魔法
            case 2:
                switch (common.leftMagicNum)
                {
                    // 火の魔法
                    case 1:
                        mixMagic = _bulletList[0].mixBullet[1];
                        break;
                    // 闇の魔法
                    case 2:
                        mixMagic = _bulletList[0].mixBullet[5];
                        break;
                    // 風の魔法
                    case 3:
                        mixMagic = _bulletList[0].mixBullet[6];
                        break;
                    // 光の魔法
                    case 4:
                        mixMagic = _bulletList[0].mixBullet[4];
                        break;
                    default:
                        break;
                }
                break;

            // 風の魔法
            case 3:
                switch (common.leftMagicNum)
                {
                    // 火の魔法
                    case 1:
                        mixMagic = _bulletList[0].mixBullet[2];
                        break;
                    // 闇の魔法
                    case 2:
                        mixMagic = _bulletList[0].mixBullet[6];
                        break;
                    // 風の魔法
                    case 3:
                        mixMagic = _bulletList[0].mixBullet[7];
                        break;
                    // 光の魔法
                    case 4:
                        mixMagic = _bulletList[0].mixBullet[9];
                        break;
                    default:
                        break;
                }
                break;
            // 光の魔法
            case 4:
                switch (common.leftMagicNum)
                {
                    // 火の魔法
                    case 1:
                        mixMagic = _bulletList[0].mixBullet[3];
                        break;
                    // 闇の魔法
                    case 2:
                        mixMagic = _bulletList[0].mixBullet[4];
                        break;
                    // 風の魔法
                    case 3:
                        mixMagic = _bulletList[0].mixBullet[9];
                        break;
                    // 光の魔法
                    default:
                        mixMagic = _bulletList[0].mixBullet[8];
                        break;
                }
                break;
            default:
                break;
        }
    }
}
