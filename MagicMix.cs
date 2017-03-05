using UnityEngine;
using Leap;
using Leap.Unity;
using System.Collections;

public class MagicMix : MonoBehaviour
{
    // 両手が合成距離に入っているかどうか
    private bool mixFrag = false;
    // 魔法合成が完了したかどうか
    private bool mixFinished = false;
    public bool _mixFinished { get { return mixFinished; } }
    // 合成している時間
    private float mixTime = 0;
    BulletList _bulletListScript;
    private Shot _shotScript;

    // 合成時のエフェクト
    public ParticleSystem mixEffect;
    // 合成できる距離
    public float beMixedDis = 0;

    // Use this for initialization
    void Start ()
    {
        _shotScript = GetComponent<Shot>();
        _bulletListScript = GameObject.Find("BulletList").GetComponent<BulletList>();
        mixEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mixCondition()
    {
        // 魔法合成の条件
        if (_shotScript.bulletRightClone != null && _shotScript.bulletLeftClone != null && common.bulletFireRight == false && common.bulletFireLeft == false)
        {
            // 両手の距離が合成可能距離より小さければ
            if (_shotScript.handsDis <= beMixedDis)
            {
                mixFrag = true;
                mixEffect.Play();
            }
            else
            {
                mixFrag = false;
                mixEffect.Stop();
                mixTime = 0;
            }
            magicMix();
        }
        else
        {
            mixEffect.Stop();
        }
    }

    // 魔法合成
    private void magicMix()
    {
        if (mixFrag == true)
        {
            mixTime += Time.deltaTime;
            if (mixTime >= 5f)
            {
                mixTime = 0;
                mixFinished = true;
                mixEffect.Stop();
                // 両手お魔法を一旦削除し、新たに右手に合成魔法をセット
                Destroy(_shotScript.bulletRightClone, 0f);
                Destroy(_shotScript.bulletLeftClone, 0f);
                // bulletRightCloneの中身をmixBulletに置き換える
                // _shotScript.bulletRight = _bulletListScript.mixMagic;
                _shotScript.bulletRightClone = GameObject.Instantiate(_bulletListScript.mixMagic, _shotScript.shotPosRight, Quaternion.identity) as GameObject;
                _shotScript.bulletRightClone.AddComponent<BulletDestroyRight>();
                // 右手の魔法が光系統ならlightフラグをtrueに
                if(_shotScript.bulletRightClone.tag == "LightMagic")
                {
                    Shot.shineFlag = true;
                }
                // 右手の魔法が光×風ならlightフラグをtrueに
                if(_shotScript.bulletRightClone.tag == "Wind_shine")
                {
                    Shot.shineFlag = false;
                }
                common.bulletFireLeft = true;
            }
        }
    }

    // 合成魔法を打ち終わった後、左の魔法を再生する
    public void LeftRevival()
    {
        if (mixFinished == true)
        {
            mixFinished = false;
            common.bulletFireLeft = false;
        }
    }
}