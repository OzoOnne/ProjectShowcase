using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class FadeIn : MonoBehaviour
{
    // vars needed for the fade in
    private RawImage rend;

    [SerializeField] private float speed = 0.2f;

    [SerializeField] private float value = 1.4f;

    void Start()
    {
        rend = GetComponent<RawImage>();
    }

    // slowy fade the game in
    void FixedUpdate()
    {
        value -= Time.fixedDeltaTime * speed;

        rend.color = new Color(0, 0, 0, value);
        if (rend.color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
