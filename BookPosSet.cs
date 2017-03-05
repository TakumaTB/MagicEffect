using UnityEngine;
using System.Collections;

public class BookPosSet : MonoBehaviour
{
    private Transform bookRightPos;
    private Transform bookLeftPos;
    public float bookHeight;

	// Use this for initialization
	void Start ()
    {
        bookRightPos = GameObject.Find("BookRight").GetComponent<Transform>();
        bookLeftPos = GameObject.Find("BookLeft").GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        bookRightPos.position = this.transform.position + new Vector3(0.5f, bookHeight, 0);
        bookLeftPos.position = this.transform.position + new Vector3(-0.5f, bookHeight, 0);
	}
}
