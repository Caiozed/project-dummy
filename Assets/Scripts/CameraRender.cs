using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraRender : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public Shader RenderShader;
    void Start()
    {
        cam = GetComponent<Camera>();
        // cam.RenderWithShader(RenderShader, null);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
