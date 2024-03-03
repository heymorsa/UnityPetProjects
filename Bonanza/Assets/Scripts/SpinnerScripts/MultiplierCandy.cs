using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SpinnerScripts
{
    public class MultiplierCandy : Slot
    {
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private ParticleSystem multiplierGlowParticle ;
        private float totalDropWeight = 0;
        protected void OnEnable()
        {
            isMultiplier = true;
            GetRandomType();
        }
    
        private void GetRandomType()
        {
            foreach (var slotType in mySlotType.multiplierTypes)
            {
                totalDropWeight += slotType.spawnWeight;
            }
            float diceRoll = Random.Range(0f, totalDropWeight);
            for (int i = 0; i < mySlotType.multiplierTypes.Count; i++)
            {
                if (diceRoll <= mySlotType.multiplierTypes[i].spawnWeight)
                {
                    slotTypeNumber = i;
                    mySlotType.OpenMultiplierImage(slotTypeNumber);
                    multiplierText.text = mySlotType.multiplierTypes[slotTypeNumber].multiplierValue + "x";
                    multiplierText.gameObject.SetActive(true);
                    break;
                }
                else
                    diceRoll -= mySlotType.multiplierTypes[i].spawnWeight;
            }
        }

        public override void Seek(Vector3 slotHolderPosition, SlotHolder slotHolder, Spinner spinner, SlotHolderParent.SlotRow slotHolderParent, float delayTime)
        {
            mySlotHolderParent = slotHolderParent;
            mySlotHolderParent.spawnedSlots.Add(this);
            mySpinner = spinner;
            var sequence = DOTween.Sequence();
            mySlotHolder = slotHolder;
            mySlotHolder.isHoldingSlot = true;
            sequence.Append(transform.DOMove(slotHolderPosition + new Vector3(0f, -70f, 0f), 0.25f).SetEase(Ease.Linear).SetDelay(delayTime).OnComplete(() => multiplierGlowParticle.Play()));
            sequence.Append(transform.DOMove(slotHolderPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => spinner.SlotCounter(slotTypeNumber,true)));
        }

        public override void Clear(float delayTime)
        {
            mySlotHolderParent.spawnedSlots.Remove(this);
            mySlotHolder.isHoldingSlot = false;
            multiplierGlowParticle.gameObject.SetActive(false);
            transform.DOMoveY(transform.position.y - 5000f, 3f).SetDelay(delayTime).OnComplete(() => Destroy(gameObject));
        }

        public override void ReList(SlotHolder slotHolder)
        {
            StartCoroutine(ReListDelay(slotHolder, slotHolder.transform));
        }

        public void MultiplierExplode(float delay,Transform multiplierTextHolder)
        {
            multiplierText.maskable = false;
            var sequence = DOTween.Sequence();
            sequence.Append(multiplierText.transform.DOScale(1.5f, 0.5f).SetDelay(delay));
            multiplierText.transform.DOScale(0f, 0.4f).SetEase(Ease.Linear).SetDelay(0.5f + delay + delay);
            sequence.Append(multiplierText.transform.DOMove(multiplierTextHolder.position, 0.4f).SetEase(Ease.Linear).SetDelay(delay).OnComplete(() =>  StartCoroutine(MultiplierExplodeDelay())));
        }
    
        IEnumerator MultiplierExplodeDelay()
        {
            yield return new WaitForSeconds(0.4f);
            MoneyController.Instance.AddMultiply(mySlotType.multiplierTypes[slotTypeNumber].multiplierValue);
            mySpinner.SlotExplodeCounter(slotTypeNumber,true);
            
        }

        IEnumerator ReListDelay(SlotHolder slotHolder,Transform slotHolderPos)
        {
            yield return new WaitForSeconds(0f);
            mySlotHolder.isHoldingSlot = false;
            slotHolder.isHoldingSlot = true;
            mySlotHolder = slotHolder;
            slotPos = slotHolderPos;
            transform.DOMove(slotPos.position, 0.3f);
        }
    }
}
