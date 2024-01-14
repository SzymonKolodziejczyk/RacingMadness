using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFlicker : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    // Update is called once per frame
    void Update()
    {
        if (SpriteRenderer != null)
        {
            SpriteRenderer.enabled = !SpriteRenderer.enabled;
        }
    }
}
