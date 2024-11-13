using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    public float _zoomInFOV = 30f;
    public float _zoomOutFOV = 60f;
    public float _zoomInSpeed = 5f;
    public float _zoomOutSpeed = 3f;
    public float _zoomInDuration = 1f;
    public float _zoomOutDuration = 1f;

    private void Awake()
    {
        CameraManager.Instance.CameraZoom = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public IEnumerator CoroutineZoomIn()
    {
        while (_virtualCamera.m_Lens.FieldOfView > _zoomInFOV)
        {
            _virtualCamera.m_Lens.FieldOfView -= _zoomInSpeed * Time.deltaTime;
            yield return null;
        }

        _virtualCamera.m_Lens.FieldOfView = _zoomInFOV;
    }

    public IEnumerator CoroutineZoomOut()
    {
        while (_virtualCamera.m_Lens.FieldOfView < _zoomOutFOV)
        {
            _virtualCamera.m_Lens.FieldOfView += _zoomOutSpeed * Time.deltaTime;
            yield return null;
        }

        _virtualCamera.m_Lens.FieldOfView = _zoomOutFOV;
    }

    public async void ZoomIn()
    {
        float startFOV = _virtualCamera.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < _zoomInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _zoomInDuration;
            _virtualCamera.m_Lens.FieldOfView = Mathf.SmoothStep(startFOV,_zoomInFOV, t);
            await Task.Yield();
        }
        //while (_virtualCamera.m_Lens.FieldOfView > _zoomInFOV)
        //{
        //    _virtualCamera.m_Lens.FieldOfView -= _zoomInSpeed * Time.deltaTime;
        //    await Task.Yield();
        //}

        _virtualCamera.m_Lens.FieldOfView = _zoomInFOV;
    }

    public async void ZoomOut()
    {
        float startFOV = _virtualCamera.m_Lens.FieldOfView;
        float elapsedTime = 0f;
        while (elapsedTime < _zoomOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _zoomOutDuration;
            _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _zoomOutFOV, t);
            await Task.Yield();
        }
        //while (_virtualCamera.m_Lens.FieldOfView < _zoomOutFOV)
        //{
        //    _virtualCamera.m_Lens.FieldOfView += _zoomOutSpeed * Time.deltaTime;
        //    await Task.Yield();
        //}

        _virtualCamera.m_Lens.FieldOfView = _zoomOutFOV;
    }
}
