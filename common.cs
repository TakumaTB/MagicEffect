using UnityEngine;
using Leap;
using Leap.Unity;
using System.Collections;

public class common : MonoBehaviour
{
    // 手の取得
    [SerializeField]
    GameObject m_ProviderObject;
    LeapServiceProvider m_Provider;

    public static Frame frame;

    // 両手の取得用
    public static Hand rightHand; // 右
    public static Hand leftHand; // 左

    // 手のひら法線
    public static Vector3 normalRight;
    // 手の位置
    public static Vector3 positionRight;
    // 指の方向？
    public static Vector3 directionRight;
    // 手の速度
    public static Vector3 velocityRight;

    // 左手の手のひら法線
    public static Vector3 normalLeft;
    // 左手の位置
    public static Vector3 positionLeft;
    // 左手の指の方向？
    public static Vector3 directionLeft;
    // 左手の速度
    public static Vector3 velocityLeft;

    // 弾表示関連のフラグ
    public static bool bulletFireRight = false;
    // public static bool bulletKeepRight = false;
    // 左手
    public static bool bulletFireLeft = false;
    // public static bool bulletKeepLeft = false;

    // カメラに表示されているかどうか
    public static bool _isRenderer = false;
    public GameObject rightHandRota;

    public Camera pCamera;
    public static Vector3 cameraForward;

    // 光の魔法の発射フラグ
    public bool lightFire = false;

    // 右手の魔法の種類
    public static GameObject bulletKindRight;
    // 左手の魔法の種類
    public static GameObject bulletKindLeft;

    private BulletList _BulletList;

    // 右の魔法の種類
    public static int rightMagicNum;
    // 左の魔法の種類
    public static int leftMagicNum;

    void awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        // 左右の魔法の種類を初期化
        rightMagicNum = 1;
        leftMagicNum = 1;
        _BulletList = GameObject.Find("BulletList").GetComponent<BulletList>();
        bulletKindRight = _BulletList._bulletList[0].normalBullet[1];
        bulletKindLeft = _BulletList._bulletList[0].normalBullet[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Provider = m_ProviderObject.GetComponent<LeapServiceProvider>();

        frame = m_Provider.CurrentFrame;

        // 右手の諸々の情報を取得
        if (rightHand != null)
        {
            // 手のひらの法線
            common.normalRight = common.rightHand.PalmNormal.ToVector3();
            // 手の位置
            common.positionRight = common.rightHand.PalmPosition.ToVector3();
            // 指の方向？
            common.directionRight = common.rightHand.Direction.ToVector3();
            // 手の速度
            common.velocityRight = common.rightHand.PalmVelocity.ToVector3();
        }

        // 左手の諸々の情報を取得
        if (leftHand != null)
        {
            // 左手の手のひらの法線
            common.normalLeft = common.leftHand.PalmNormal.ToVector3();
            // 左手の位置
            common.positionLeft = common.leftHand.PalmPosition.ToVector3();
            // 左手の指の方向
            common.directionLeft = common.leftHand.Direction.ToVector3();
            // 左手の速度
            common.velocityLeft = common.leftHand.PalmVelocity.ToVector3();
        }
    }

    // 右手の取得
    public static void getRightHand()
    {
        rightHand = null;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                rightHand = hand;
                break;
            }
        }
    }

    // 左手の取得
    public static void getLeftHand()
    {
        leftHand = null;
        foreach (Hand handL in frame.Hands)
        {
            if (handL.IsLeft)
            {
                leftHand = handL;
                break;
            }
        }
    }
}
