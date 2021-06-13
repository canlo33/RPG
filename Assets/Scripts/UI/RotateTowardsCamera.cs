using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateTowardsCamera : MonoBehaviour
{
    private TextMeshPro damageText;
    private float minTextSize = 4f;
    private float maxTextSize = 8f;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponentInChildren<TextMeshPro>();
        player = GameMaster.instance.player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (!damageText.enabled)
            return;
        float currentDistance = Vector3.Distance(transform.position, player.position);      
        damageText.fontSize = minTextSize + (currentDistance / (maxTextSize - minTextSize));
        transform.LookAt(Camera.main.transform.position);
    }
}
