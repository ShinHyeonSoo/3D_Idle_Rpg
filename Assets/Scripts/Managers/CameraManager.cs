using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraManager : Singleton<CameraManager>
{
    public CameraZoom CameraZoom { get; set; }

    public List<Camera> _ovetlayCameras;


    protected override void Awake()
    {
        base.Awake();
        _ovetlayCameras = GetOverlayCameras(Camera.main);
    }

    List<Camera> GetOverlayCameras(Camera baseCam)
    {
        List<Camera> overlayCameras = new List<Camera>();
        if (baseCam != null)
        {
            UniversalAdditionalCameraData cameraData = baseCam.GetComponent<UniversalAdditionalCameraData>();
            if (cameraData != null)
            {
                // Overlay 카메라 목록을 가져오기
                cameraData.cameraStack.ForEach(cam => overlayCameras.Add(cam));
            }
        }
        return overlayCameras;
    }
}
