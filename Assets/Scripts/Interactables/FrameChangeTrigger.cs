using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FrameChangeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string AreaName;
    public CinemachineVirtualCamera FrameCamera;
    public CinemachineVirtualCamera PreviousCamera;
    public CinemachineVirtualCamera NextCamera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ChangeCamera();
            UIManager.Instance.SetText(AreaName);
            MasterManager.Instance.TriggerAnim("LevelTransition");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ChangeCamera();
        }
    }


    public void ChangeCamera()
    {
        FrameCamera.enabled = true;
        if (PreviousCamera != null)
            PreviousCamera.enabled = false;
        if (NextCamera != null)
            NextCamera.enabled = false;
    }
}
