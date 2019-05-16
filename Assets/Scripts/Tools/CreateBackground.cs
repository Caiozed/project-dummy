using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackground : MonoBehaviour
{
    PolygonCollider2D polyCollider;
    public GameObject backgroundObj;
    // Start is called before the first frame update
    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        var obj = Instantiate(backgroundObj, polyCollider.bounds.center, transform.rotation);
        obj.transform.localScale = new Vector3(polyCollider.bounds.size.x / 5f, polyCollider.bounds.size.y / 2, polyCollider.bounds.max.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}