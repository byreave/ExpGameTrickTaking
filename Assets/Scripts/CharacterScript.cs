using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    //0 nothing 1 fix 2 fish 3 pray
    public int TodayJob = 0;
    public Text JobText;
    public bool isBad = false;
    [SerializeField]
    float RotateAngle = 30.0f;
    // Start is called before the first frame update
    [SerializeField]
    Transform parentObj;
    float RotateSpeed = 90.0f;
    void Start()
    {
        //parentObj = gameObject.GetComponentInParent<Transform>();
        //Debug.Log(parentObj.name);
        //transform.RotateAround(parentObj.position, Vector3.up, RotateAngle);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartRotateNext()
    {
        for(float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            transform.RotateAround(parentObj.position, Vector3.up, RotateSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Show()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
