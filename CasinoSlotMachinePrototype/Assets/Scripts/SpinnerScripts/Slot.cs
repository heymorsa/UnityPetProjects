using DG.Tweening;
using UnityEngine;

namespace SpinnerScripts
{
    public abstract class Slot : MonoBehaviour
    {
        [SerializeField] protected SlotType mySlotType;
        protected Vector3 startPos;
        public bool isMultiplier;
        public Transform slotPos;
        public SlotHolder mySlotHolder;
    
        public int slotTypeNumber;
    
        protected Spinner mySpinner;
    
        public ParticleSystem explodeParticle;
        protected SlotHolderParent.SlotRow mySlotHolderParent;

        public DOTweenAnimation myAnim;
        public Animator myAnimator;
    
        public abstract void Seek(Vector3 slotHolderPosition, SlotHolder slotHolder, Spinner spinner, SlotHolderParent.SlotRow slotHolderParent, float delayTime);

        public abstract void Clear(float delayTime);

        public abstract void ReList(SlotHolder slotHolder);

    
    
    
    
    }
}
