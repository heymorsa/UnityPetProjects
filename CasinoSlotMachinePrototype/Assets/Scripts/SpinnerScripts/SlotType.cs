using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpinnerScripts
{
    public class SlotType : MonoBehaviour
    {
        [System.Serializable]
        public struct Type
        {
            public int typeNo;
            public int moneyValue;
            public float spawnWeight;
            public Sprite typeImage;
        }
    
        [System.Serializable]
        public struct MultiplierType
        {
            public int typeNo;
            public int multiplierValue;
            public float spawnWeight;
            public Sprite typeImage;
        }
    
        public List<Type> slotTypes;
        public List<MultiplierType> multiplierTypes;
        public Image slotImage;
        public void OpenImage(int slotType)
        {
            slotImage.sprite = slotTypes[slotType].typeImage;
        }
    
        public void OpenMultiplierImage(int multiplierType)
        {
            slotImage.sprite = multiplierTypes[multiplierType].typeImage;
        }

    }
}
