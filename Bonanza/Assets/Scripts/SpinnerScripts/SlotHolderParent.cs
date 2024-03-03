using System.Collections.Generic;
using UnityEngine;

namespace SpinnerScripts
{
    public class SlotHolderParent : MonoBehaviour
    {
    
        public static SlotHolderParent Instance;
    
        [System.Serializable]
        public class SlotRow
        {
            public int rowNo;
            public Transform rowParent;
            public List<SlotHolder> slotHolders;
            public Transform spawnPos;
            public List<Slot> spawnedSlots;
        }
    
        public List<SlotRow> rows;

        private void Awake()
        {
            Instance = this;
            foreach (var slotRow in rows)
            {
                int maxIndex = slotRow.rowParent.childCount - 1;
                for (int i = 0; i <= maxIndex; i++)
                {
                    slotRow.slotHolders.Add(slotRow.rowParent.GetChild(maxIndex - i).GetComponent<SlotHolder>());
                }
            }
        }
    }
}
