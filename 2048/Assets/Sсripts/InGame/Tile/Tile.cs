using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Tile {
    public class Tile :MonoBehaviour {
        public TileState state { get; private set; }
        public TileCell cell { get; private set; }
        public bool locked { get; set; }
        public List<GameObject> ObjectsList;

        private GameObject prefabGameObject;
        private TextMeshProUGUI text;
        private AudioSource audioSource;



        [SerializeField] Canvas canvas;
        InGameManager inGameManager;
        private void Awake() {
            prefabGameObject = GetComponent<GameObject>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            inGameManager = FindObjectOfType<InGameManager>();
            audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable() {
          //  canvas.worldCamera = inGameManager.GetComponentInChildren<Camera>();
        }
        public void SetState(TileState state) {
            this.state = state;
            foreach(GameObject o in ObjectsList) {
                o.SetActive(false);
            }
            ObjectsList[state.count].SetActive(true);
            inGameManager.SetCountState(state.count);

        }

        public void Spawn(TileCell cell) {
            if (this.cell != null) {
                this.cell.tile = null;
            }

            this.cell = cell;
            this.cell.tile = this;
            Vector3 newPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, -0.33f);
            transform.position = newPosition;
        }

        public void MoveTo(TileCell cell) {
            if (this.cell != null) {
                this.cell.tile = null;
            }

            this.cell = cell;
            this.cell.tile = this;
            Vector3 newPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, -0.33f);

            StartCoroutine(Animate(newPosition, false));
        }

        public void Merge(TileCell cell) {
            if (this.cell != null) {
                this.cell.tile = null;
            }

            this.cell = null;
            cell.tile.locked = true;
            Vector3 newPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, -0.33f);
            audioSource.Play();
            StartCoroutine(Animate(newPosition, true));
        }

        private IEnumerator Animate(Vector3 to, bool merging) {
            float elapsed = 0f;
            float duration = 0.1f;

            Vector3 from = transform.position;

            while (elapsed < duration) {
                transform.position = Vector3.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = to;

            if (merging) {
                Destroy(gameObject);
            }
        }
    }
}