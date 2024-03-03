using UnityEngine;

namespace Aviator.Code.Core.Resolution
{
    public class CameraScale : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _iPadSize;
        [SerializeField] private float _iPhoneSize;

        private void Start()
        {

          if (Screen.width == 2732)
          {
            _camera.orthographicSize = _iPadSize;
          }
           else if ( Screen.width == 2048)
           {
             _camera.orthographicSize = _iPadSize;
           } else
           {
             _camera.orthographicSize = _iPhoneSize;
           }
        }
            
    }
}