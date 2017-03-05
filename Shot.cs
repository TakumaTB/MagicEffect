using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Shot : MonoBehaviour
{
    // 弾の発射位置
    public Vector3 shotPosRight;
    // 左
    Vector3 shotPosLeft;

    // 弾の弾道補正
    private Vector3 shotDirectionRight;
    private Vector3 shotDirectionLeft;

    // velocityの平均値
    public float velocityAveRight = 0;
    public float velocityAveLeft = 0;

    // 弾速
    public float speed;

    // 次弾装填時間
    private float timeRight = 0f;
    public float intervalRight = 1.0f;
    // 左手用
    private float timeLeft = 0f;
    public float intervalLeft = 1.0f;

    // 弾が発射可能な速度
    public float shotNum;

    // 両手のオブジェクト
    public GameObject rightObj;
    public GameObject leftObj;

    // 撃った弾を格納するオブジェクト
    public GameObject bulletBox;

    // 弾のプレハブ、弾のクローン
    public GameObject bulletRight;
    public GameObject bulletRightClone;
    // 左手の弾
    private GameObject bulletLeft;
    public GameObject bulletLeftClone;

    // 右の本
    SelectRight _selectRight;
    // 左の本
    SelectLeft _selectLeft;
    // 弾のリスト
    BulletList _bulletListScript;

    // 両手の距離
    public float handsDis;

    // 合成エフェクトの位置
    private Vector3 effectPos;

    // 右手のオブジェクトを入れておく箱
    private GameObject Bullet;

    // MagicMix読み込み用
    MagicMix _MagicMix;

    //光魔法フラグ
    // 右手用
    public static bool shineFlag = false;
    // 左手用
    public static bool shineFlag_left = false;
    // リロード用スライダー
    // 右手
    public GameObject gauge_right;
    // 左手
    public GameObject gauge_left;

    // Use this for initialization
    void Start()
    {
        _bulletListScript = GameObject.Find("BulletList").GetComponent<BulletList>();
        _MagicMix = GetComponent<MagicMix>();
        // slider = GameObject.Find("Slider");
        // reloadSlider = slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(common.bulletFireRight);
        common.getRightHand();
        common.getLeftHand();

        rightHandCtrl();
        leftHandCtrl();

        if(common.bulletFireRight == false)
        {
            gauge_right.SetActive(false);
        }
        if(common.bulletFireLeft == false)
        {
            gauge_left.SetActive(false);
        }

        effectPos = new Vector3(common.normalRight.x, common.normalRight.y + 0.3f, common.normalRight.z) / 10;
        if (common.rightHand == null)
        {
            return;
        }
        else
        {
            common.positionRight = common.rightHand.PalmPosition.ToVector3();
            _MagicMix.mixEffect.transform.position = common.positionRight + effectPos;
        }

        handsDis = Vector3.Distance(common.positionRight, common.positionLeft);

        _MagicMix.mixCondition();
        LightPosSet_right();
        LightPosSet_left();
    }

    void rightHandCtrl()
    {
        // 右手制御用
        if (common.rightHand != null)
        {
            shotPosRight = new Vector3(common.normalRight.x, common.normalRight.y + 0.3f, common.normalRight.z) / 10;
            shotDirectionRight = common.normalRight;

            // 弾の表示
            if (bulletRightClone == null && common.bulletFireRight == false)
            {
                bulletRightClone = Instantiate(common.bulletKindRight, this.transform.position, Quaternion.identity) as GameObject;
                bulletRightClone.AddComponent<BulletDestroyRight>();
                if (bulletRightClone.tag == "LightMagic")
                {
                    shineFlag = true;
                }
                else
                {
                    shineFlag = false;
                }
            }
            if (bulletRightClone != null && common.bulletFireRight == false)
            {
                bulletRightClone.transform.parent = rightObj.transform;
                bulletRightClone.transform.position = common.positionRight + shotPosRight;
            }

            // velocityの絶対値を平均する
            velocityAveRight = (Mathf.Abs(common.velocityRight.x) + Mathf.Abs(common.velocityRight.y) + Mathf.Abs(common.velocityRight.z)) / 3;

            shot();
            Right_bulletFireCheck();
        }
    }

    void leftHandCtrl()
    {
        // 左手の制御
        if (common.leftHand != null)
        {
            // shotPos = new Vector3(position.x, position.y, position.z);
            shotPosLeft = common.positionLeft + new Vector3(common.normalLeft.x, common.normalLeft.y + 0.3f, common.normalLeft.z) / 10;

            // normalをshotDirectionに代入
            shotDirectionLeft = common.normalLeft;

            if (_MagicMix._mixFinished == false)
            {
                // 左手の弾の表示
                if (bulletLeftClone == null && common.bulletFireLeft == false)
                {
                    bulletLeftClone = Instantiate(common.bulletKindLeft, shotPosLeft, Quaternion.identity) as GameObject;
                    bulletLeftClone.AddComponent<BulletDestroyLeft>();
                    if (bulletLeftClone.tag == "LightMagic")
                    {
                        shineFlag_left = true;
                    }
                    else
                    {
                        shineFlag_left = false;
                    }
                }
                if (bulletLeftClone != null && common.bulletFireLeft == false)
                {
                    bulletLeftClone.transform.parent = leftObj.transform;
                    bulletLeftClone.transform.position = shotPosLeft;
                }
            }

            // velocityの絶対値を平均する
            velocityAveLeft = (Mathf.Abs(common.velocityLeft.x) + Mathf.Abs(common.velocityLeft.y) + Mathf.Abs(common.velocityLeft.z)) / 3;

            if (_MagicMix._mixFinished == false)
            {
                shotL();
                Left_BulletFireCheck();
            }
        }
    }

    // 右手の魔法が光系統だったらpositionを設定
    void LightPosSet_right()
    {
        if (common.rightHand != null && bulletRightClone != null && bulletRightClone.tag == "LightMagic")
        {
            // palmObject_right = GameObject.FindGameObjectWithTag("HandRight").GetComponent<Transform>();
            // Vector3 palmF_right = palmObject_right.transform.up;
            bulletRightClone.transform.forward = common.directionRight;
            bulletRightClone.transform.position = common.positionRight + new Vector3(common.normalRight.x, common.normalRight.y + 0.3f, common.normalRight.z) / 10;
        }
    }
    // 左手の魔法が光系統だったらpositionを設定
    void LightPosSet_left()
    {
        if (common.leftHand != null && bulletLeftClone != null && bulletLeftClone.tag == "LightMagic")
        {
            // palmObject_left = GameObject.FindGameObjectWithTag("HandLeft").GetComponent<Transform>();
            // Vector3 palmF_left = palmObject_left.transform.up;
            bulletLeftClone.transform.forward = common.directionLeft;
            bulletLeftClone.transform.position = common.positionLeft + new Vector3(common.normalLeft.x, common.normalLeft.y + 0.3f, common.normalLeft.z) / 10;
        }
    }

    // 右手のshot
    void shot()
    {
        // velocityAveがshotNum以上になったら発射
        if (velocityAveRight >= shotNum && bulletRightClone != null && common.bulletFireRight == false /*&& shotDirectionRight.z > 0*/)
        {
            common.bulletFireRight = true;
            gauge_right.SetActive(true);
            if (shineFlag == false)
            {
                // 飛ばす方向の補正
                if (shotDirectionRight.y <= -0.1)
                {
                    shotDirectionRight.y = 0;
                }
                bulletRightClone.GetComponent<Rigidbody>().AddForce(shotDirectionRight * speed);
                bulletRightClone.transform.parent = bulletBox.transform;
            }
            if (shineFlag == true)
            {
                Extend_collider scaleScript = bulletRightClone.GetComponent<Extend_collider>();
                scaleScript.ScaleUpStart_right();
            }

            _MagicMix.LeftRevival();
        }
    }

    // 左手のshot
    void shotL()
    {
        // 左手のvelocityLAveがshotNum以上になったら発射
        if (velocityAveLeft >= shotNum && bulletLeftClone != null && common.bulletFireLeft == false /*&& shotDirectionLeft.z > 0*/)
        {
            common.bulletFireLeft = true;
            gauge_left.SetActive(true);
            if (shineFlag_left == false)
            {
                // 飛ばす方向の補正
                if (shotDirectionLeft.y <= -0.1)
                {
                    shotDirectionLeft.y = 0;
                }
                bulletLeftClone.GetComponent<Rigidbody>().AddForce(shotDirectionLeft * speed);
                bulletLeftClone.transform.parent = bulletBox.transform;
            }
            if (shineFlag_left == true)
            {
                Extend_collider scaleScript = bulletLeftClone.GetComponent<Extend_collider>();
                scaleScript.ScaleUpStart_left();
            }
        }
    }

    /// <summary>
    /// 右手の弾が飛んだ後にbulletListの子オブジェクトにして制御を移す
    /// </summary>
    void Right_bulletFireCheck()
    {
        if (common.bulletFireRight == true)
        {
            timeRight += Time.deltaTime;
            gauge_right.GetComponent<Slider>().value = timeRight;
            if (timeRight >= intervalRight)
            {
                timeRight = 0;
                common.bulletFireRight = false;
                gauge_right.GetComponent<Slider>().value = 0;
                gauge_right.SetActive(false);
            }
            if (shineFlag == true)
            {
                Extend_collider.shineFire_right = false;
            }

            /*
            reloadSlider.value = timeRight;
            if(reloadSlider.value >= reloadSlider.maxValue)
            {
                reloadSlider.value = 0;
            }
            */
        }
    }

    /// <summary>
    /// 左手の弾が飛んだ後にbulletListの子オブジェクトにして制御を移す
    /// </summary>
    void Left_BulletFireCheck()
    {
        if (common.bulletFireLeft == true && _MagicMix._mixFinished == false)
        {
            timeLeft += Time.deltaTime;
            gauge_left.GetComponent<Slider>().value = timeLeft;
            if (timeLeft >= intervalLeft)
            {
                timeLeft = 0;
                common.bulletFireLeft = false;
                gauge_left.GetComponent<Slider>().value = 0;
                gauge_left.SetActive(false);
            }
            if (shineFlag_left == true)
            {
                Extend_collider.shineFire_left = false;
            }
        }
    }
}