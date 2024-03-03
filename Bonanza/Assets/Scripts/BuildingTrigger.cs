using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] private Image circularImage;
    [SerializeField] private GameObject buildArea;
    [SerializeField] private GameObject myCanvas;
    [SerializeField] private BoxCollider myCollider;
    private bool isPlayerTop,isSpinnerActivated;
    private float circleTime = 0f;
    
    [SerializeField] private float cost;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Transform particlePosition;
    private void Start()
    {
        costText.text = cost.ToString();
    }

    private void Update()
    {
        if (MoneyController.Instance.HaveMoney(cost))
        {
            if (isPlayerTop && circleTime <= 2.1f)
            {
                circleTime += Time.deltaTime;
                circularImage.fillAmount = circleTime / 2f;
            }
            if (circleTime >= 2f && !isSpinnerActivated)
            {
                isSpinnerActivated = true;
                buildArea.SetActive(true);
                myCanvas.SetActive(false);
                myCollider.enabled = false;
                ParticleManager.Instance.SpawnBuildingParticle(particlePosition.position);
                MoneyController.Instance.SpendMoney(200);
            }
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
