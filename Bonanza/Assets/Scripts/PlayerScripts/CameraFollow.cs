using UnityEngine;

namespace PlayerScripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform obj;
        public Vector3 offSet;
     
        private void Update()
        {
            transform.position = obj.position + offSet;
        }
    }
}
