using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Tile {
    public class TileInfo :MonoBehaviour {
        public List<GameObject> ObjectsList;
        public void Init() {
            for(int i = 1; i < ObjectsList.Count; i++) {
                ObjectsList[i].SetActive(false);
            }
        }
        // Update is called once per frame
        public void OpenInfo(int openFigure) {
            if(openFigure <= ObjectsList.Count) {
                ObjectsList[openFigure].SetActive(true);
            }
        }
    }
}