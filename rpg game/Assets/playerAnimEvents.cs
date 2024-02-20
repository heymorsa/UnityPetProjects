using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimEvents : MonoBehaviour
{
    private player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<player>();
    }

    private void AnimationTrigger()
    {
        player.AttackOver();
    }
}
