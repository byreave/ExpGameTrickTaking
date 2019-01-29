using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    float RotateAngle = 30.0f;
    // Start is called before the first frame update
    [SerializeField]
    Transform parentObj;
    float RotateSpeed = 90.0f;
    void Start()
    {
        //parentObj = gameObject.GetComponentInParent<Transform>();
        Debug.Log(parentObj.name);
        //transform.RotateAround(parentObj.position, Vector3.up, RotateAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            StartCoroutine(StartRotateNext());

    }

    IEnumerator StartRotateNext()
    {
        for(float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            transform.RotateAround(parentObj.position, Vector3.up, RotateSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
