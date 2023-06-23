using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [SerializeField] private GameObject m_camera1Object, m_camera2Object;
    [SerializeField] private GameObject m_aerialView1, m_aerialView2, m_aerialViews;
    private bool cameraChange;
    private void Start()
    {
        m_aerialView1 = GameObject.Find("AerialViewContainerP1");
        m_aerialView2 = GameObject.Find("AerialViewContainerP2");
        m_aerialViews = GameObject.Find("AerialViews");

        m_camera1Object.SetActive(true);
        m_camera2Object.SetActive(false);
        m_aerialView1.SetActive(true);
        m_aerialView2.SetActive(false);
        m_aerialViews.SetActive(true);
    }
    public void CameraChangeEnabled(bool changeEnabled)
    {
            cameraChange = changeEnabled;
    }
    public void AlternateGameCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) && cameraChange)
        {
            AlternateCameras();
            AlternateAerialView();
        }
    }
    private void AlternateCameras()
    {
        m_camera1Object.SetActive(!m_camera1Object.activeSelf);
        m_camera2Object.SetActive(!m_camera2Object.activeSelf);
    }
    private void AlternateAerialView()
    {
        m_aerialView1.SetActive(!m_aerialView1.activeSelf);
        m_aerialView2.SetActive(!m_aerialView2.activeSelf);
    }
    private void Update()
    {
        AlternateGameCamera();
    }
}
