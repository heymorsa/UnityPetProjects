using UnityEngine;
using UnityEngine.UI;

namespace SpinnerScripts
{
    public class OpenSpinner : MonoBehaviour
    {
        [SerializeField] private Image circularImage;
        [SerializeField] private GameObject spinner;
        private bool isPlayerTop,isSpinnerActivated;
        private float circleTime = 0f;
    
        private void Update()
        {
            if (isPlayerTop && circleTime <= 2.1f)
            {
                circleTime += Time.deltaTime;
                circularImage.fillAmount = circleTime / 2f;
            }
            if (circleTime >= 2f && !isSpinnerActivated)
            {
                isSpinnerActivated = true;
                spinner.SetActive(true);
                MoneyController.Instance.ResetWin();
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            isPlayerTop = true;
        }
    
        private void OnTriggerExit(Collider other)
        {
            isPlayerTop = false;
            circleTime = 0f;
            circularImage.fillAmount = 0f;
            if (isSpinnerActivated)
                isSpinnerActivated = false;
        }
    
    }
}
