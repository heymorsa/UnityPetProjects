using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SpinnerScripts
{
    public class Spinner : MonoBehaviour
    {
        public float basicSlotDropWeight, basicMultiplierDropWeight;
        private float totalDropWeight;
    
        public GameObject basicSlotPrefab,basicMultiplierPrefab;
        private int readySlotCount = 0;
        public static event Action<int,int> OnSlotExploded;
    
        public Transform parentCanvas;
        private bool canSpin,firstSpin;
    
        private int multiplierCount;
        private bool canMultiplierExplode;
        private int[] slotCounter = new int[3];
    
        [SerializeField] private DOTweenAnimation buttonAnim;
        [SerializeField] private Button spinButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private MoneyController moneyController;
        [SerializeField] private TextMeshProUGUI multiplierTextHolder;

        private int betCost;
        [SerializeField] private TextMeshProUGUI betText;
        private void Start()
        {
            canMultiplierExplode = false;
            multiplierCount = 0;
            canSpin = true;
            firstSpin = true;
            betCost = 5;
        }
    
        private void OnDisable()
        {
            MoneyController.Instance.SetMainMoney();
        }

        public void Spin()
        {
            if(moneyController.HaveMoney(betCost))
                if (canSpin)
                {
                    moneyController.SpendMoney(betCost);
                    moneyController.ResetWin();
                    buttonAnim.DOPlay();
                    spinButton.interactable = false;
                    exitButton.interactable = false;
                    float delayTime = 0.5f;
                    readySlotCount = 0;
                    for (int i = 0; i < slotCounter.Length; i++)
                    {
                        slotCounter[i] = 0;
                    }
                    if (!firstSpin)
                    {
                        ClearSlots();
                    }
                    foreach (var slotRow in SlotHolderParent.Instance.rows)
                    {
                        StartCoroutine(SpawnSlot(slotRow.slotHolders, slotRow.spawnPos,slotRow, delayTime));
                        delayTime += 0.3f;
                    }
                    canSpin = false;
                    firstSpin = false;
                }
        }
    
        public void SetBetCost(bool increase)
        {
            if (increase)
                betCost += 5;
            else if (betCost >= 5)
                betCost -= 5;
            betText.text = betCost.ToString();
        }

        private GameObject GetRandomSlot()
        {
            totalDropWeight = basicSlotDropWeight + basicMultiplierDropWeight;
            float diceRoll = Random.Range(0f, totalDropWeight);
            return basicSlotDropWeight >= diceRoll ? basicSlotPrefab : basicMultiplierPrefab;
        }
    
        private void ClearSlots()
        {
            float delayTime = 0f;
            foreach (var slotRow in SlotHolderParent.Instance.rows)
            {
                StartCoroutine(ClearSlot(slotRow.spawnedSlots, delayTime));
                delayTime += 0.25f;
            }
        }
        public void SlotCounter(int slotType,bool multiplier)
        {
            readySlotCount++;
            if(!multiplier)
                slotCounter[slotType]++;
            if (readySlotCount == 20)
            {
                ExplodeSlots();
            }
        }
        public void SlotExplodeCounter(int slotType,bool multiplier)
        {
            readySlotCount--;
            if (!multiplier)
                slotCounter[slotType]--;
            else
            {
                multiplierCount--;
                if (multiplierCount == 0)
                {
                    moneyController.MultiplyMoney();
                    canSpin = true;
                    spinButton.interactable = true;
                    exitButton.interactable = true;
                    canMultiplierExplode = false;
                }
            }
        }
        private void ExplodeSlots()
        {
            var index = 0;
            for (int i = 0; i < slotCounter.Length; i++)
            {
                if (slotCounter[i] > 8)
                {
                    OnSlotExploded?.Invoke(i,betCost);
                    if(!canMultiplierExplode)
                        canMultiplierExplode = true;
                }
                else
                    index++;
            }
            if (index == 3)
            {
                if (canMultiplierExplode)
                {
                    MultiplierSlotExplode();
                }
                else
                {
                    canSpin = true;
                    spinButton.interactable = true;
                    exitButton.interactable = true;
                }
            }
            else
            {
                foreach (var slotRow in SlotHolderParent.Instance.rows)
                    StartCoroutine(RelistSlot(slotRow.spawnedSlots, slotRow.slotHolders));
                foreach (var slotRow in SlotHolderParent.Instance.rows)
                    StartCoroutine(SpawnSlot(slotRow.slotHolders, slotRow.spawnPos,slotRow,1.55f));   
            }
        }

        private void MultiplierSlotExplode()
        {
            var delayTime = 0.5f;
            foreach (var slotRow in SlotHolderParent.Instance.rows)
            {
                foreach (var spawnedSlot in slotRow.spawnedSlots)
                {
                    if (spawnedSlot.isMultiplier)
                    {
                        multiplierCount++;
                        spawnedSlot.GetComponent<MultiplierCandy>().MultiplierExplode(delayTime,multiplierTextHolder.transform);
                        delayTime += 0.4f;
                    }
                }
            }
            if (multiplierCount == 0)
            {
                canSpin = true;
                spinButton.interactable = true;
            }
        }
    
        IEnumerator SpawnSlot(List<SlotHolder> slotHolderList,Transform spawnPos,SlotHolderParent.SlotRow slotRow,float delay)
        {
            yield return new WaitForSeconds(delay);
            var delayTime = 0.1f;
            foreach (var slotHolder in slotHolderList)
            {
                if (!slotHolder.isHoldingSlot)
                {
                    GameObject spawningSlot = Instantiate(GetRandomSlot(), spawnPos.position,transform.rotation,parentCanvas);
                    spawningSlot.GetComponent<Slot>().Seek(slotHolder.transform.position,slotHolder,this,slotRow,delayTime);
                    delayTime += 0.15f;
                }
            }
        }
        IEnumerator ClearSlot(List<Slot> spawnedSlots,float delay)
        {
            var delayTime = 0.1f;
            var maxIndex = spawnedSlots.Count;
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < maxIndex; i++)
            {
                spawnedSlots[0].Clear(delayTime);
                delayTime += 0.05f;
            }
        }
        IEnumerator RelistSlot(List<Slot> spawnedSlotsList,List<SlotHolder> slotHolderList)
        {
            yield return new WaitForSeconds(1.45f);
            int slotHolderIndex = 0;
            if (spawnedSlotsList.Count < 5)
            {
                foreach (var slot in spawnedSlotsList)
                {
                    slot.ReList(slotHolderList[slotHolderIndex]);
                    slotHolderIndex++;
                }
            }
        }
    }
}
