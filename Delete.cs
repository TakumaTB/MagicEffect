using UnityEngine;
using System.Collections;

public class Delete : MonoBehaviour
{
    private Renderer ObjRenderer;
    public float deleteTime = 0.05f;
    Color a;
    Color ObjAlpha;
    bool JustOnce = false;

	// Use this for initialization
	void Start ()
    {
        ObjRenderer = this.GetComponent<Renderer>();
        a = new Color(0, 0, 0, deleteTime);

    }
	
	// Update is called once per frame
	void Update ()
    {
        // このゲームオブジェクトのアルファ値が以上ならマイナス、0以下ならプラスする
        if(ObjRenderer.material.color.a >= 0)
        {
            ObjRenderer.material.color -= a;
        }
        if(ObjRenderer.material.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    /*
    private IEnumerator FadeOut()
    {
        for(float time = 1.0f; time >= 0; time -= 0.1f)
        {
            ObjAlpha = ObjRenderer.material.color;
            ObjAlpha.a = time;
            ObjRenderer.material.color = ObjAlpha;
            Debug.Log(ObjRenderer.material.color.a);
        }
        yield return null;
    }
    */
}
