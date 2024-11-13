using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public CameraZoom CameraZoom { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }
}
