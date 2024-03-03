using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SpinnerScripts
{
    public class BasicCandy : Slot
    {
        private float totalDropWeight = 0;
        private int betMultiplier = 0;
        protected void OnEnable()
        {
            Spinner.OnSlotExploded += Explode;
            GetRandomType();
        }

        protected void OnDisable()
        { 
            Spinner.OnSlotExploded -= Explode;
        }
    
        private void GetRandomType()
        {
            foreach (var slotType in mySlotType.slotTypes)
            {
                totalDropWeight += slotType.spawnWeight;
            }
            float diceRoll = Random.Range(0f, totalDropWeight);
            for (int i = 0; i < mySlotType.slotTypes.Count; i++)
            {
                if (diceRoll <= mySlotType.slotTypes[i].spawnWeight)
                {
                    slotTypeNumber = i;
                    mySlotType.OpenImage(slotTypeNumber);
                    break;
                }
                else
                    diceRoll -= mySlotType.slotTypes[i].spawnWeight;
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
            sequence.Append(transform.DOMove(slotHolderPosition + new Vector3(0f, -70f, 0f), 0.25f).SetEase(Ease.Linear).SetDelay(delayTime).OnComplete(() => myAnimator.SetTrigger("Pressed")));
            sequence.Append(transform.DOMove(slotHolderPosition, 0.1f).SetEase(Ease.Linear).OnComplete(() => spinner.SlotCounter(slotTypeNumber,false)));
        }

        public override void Clear(float delayTime)
        {
            mySlotHolderParent.spawnedSlots.Remove(this);
            mySlotHolder.isHoldingSlot = false;
            Spinner.OnSlotExploded -= Explode;
            transform.DOMoveY(transform.position.y - 5000f, 3f).SetDelay(delayTime).OnComplete(() => Destroy(gameObject));
        }

        public override void ReList(SlotHolder slotHolder)
        {
            StartCoroutine(ReListDelay(slotHolder, slotHolder.transform));
        }

        private void Explode(int explodingSlotNo,int betCost)
        {
            if (explodingSlotNo == slotTypeNumber)
            {
                betMultiplier = betCost;
                mySlotHolderParent.spawnedSlots.Remove(this);
                mySlotHolder.isHoldingSlot = false;
                myAnim.DOPlay();
                explodeParticle.Play();
                transform.DOScale(0.9f, 0.5f).OnComplete(() => StartCoroutine(SlotExplodeDelay()));
            }
        }
    
        IEnumerator SlotExplodeDelay()
        {
            yield return new WaitForSeconds(0.65f);
            explodeParticle.Stop();
            ParticleManager.Instance.SpawnDestroySlotParticle(transform.position);
            MoneyController.Instance.EarnMoney(mySlotType.slotTypes[slotTypeNumber].moneyValue * betMultiplier);
            mySpinner.SlotExplodeCounter(slotTypeNumber,false);
            Destroy(gameObject);
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
