using TMPro;
using UnityEngine;

public class KeyUI : MonoBehaviour, IKeyObserver
{
    private TextMeshProUGUI keyText;

    private void Start()
    {
        keyText = GetComponent<TextMeshProUGUI>();
        if (keyText == null)
        {
            return;
        }

        Key.RegisterKeyObserver(this);
        OnKeyCollected(0);
    }

    private void OnDestroy()
    {
        Key.UnregisterKeyObserver(this);
    }

    public void OnKeyCollected(int keyCount)
    {
        keyText.text = keyCount + "/4";
    }
}