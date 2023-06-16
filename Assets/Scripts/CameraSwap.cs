using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [SerializeField] private GameObject m_camera1Object, m_camera2Object;
    private bool cameraChange;
    private void Start()
    {
        m_camera1Object.SetActive(true);
        m_camera2Object.SetActive(false);
    }
    public void CameraChangeEnabled(bool changeEnabled)
    {
            cameraChange = changeEnabled;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && cameraChange)
        {
            AlternateCameras();
        }
    }
    private void AlternateCameras()
    {
        m_camera1Object.SetActive(!m_camera1Object.activeSelf);
        m_camera2Object.SetActive(!m_camera2Object.activeSelf);
    }
}
