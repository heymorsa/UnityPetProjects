using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.EnvironmentGen {
    public class Environment :MonoBehaviour {
        [SerializeField] List<GameObject> En;

        public void Init() {
            NewGame();
        }

        public void NewGame() {
            int i = Random.Range(0, En.Count);
            for (int j = 0; j < En.Count; j++) {
                if (j == i) En[j].SetActive(true);
                else En[j].SetActive(false);
            }
        }
    }
}