using UnityEngine;
namespace Game.Tile {
    public class TileCell :MonoBehaviour {
        public Vector2Int coordinates { get; set; }
        public Tile tile { get; set; }
        public bool empty => tile == null;
        public bool occupied => tile != null;
    }
}