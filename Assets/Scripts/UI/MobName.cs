using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobName : MonoBehaviour
{
    private TextMeshPro mobNameTxt;
    public float minTextSize;
    public float maxTextSize;
    public float maxDisplayDistance;
    private Transform player;
    private Entity entity;
    // Start is called before the first frame update
    void Start()
    {
        mobNameTxt = GetComponentInChildren<TextMeshPro>();
        player = GameMaster.instance.player.transform;
        mobNameTxt.SetText(transform.parent.name);
        mobNameTxt.gameObject.SetActive(false);
        entity = GetComponentInParent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        TextActivation();
    }

    void TextActivation()
    {
        if (Vector3.Distance(transform.position, player.position) < maxDisplayDistance && !entity.IsEnemyDead())
        {
            float currentDistance = Vector3.Distance(transform.position, player.position);
            mobNameTxt.gameObject.SetActive(true);
            mobNameTxt.fontSize = minTextSize + (currentDistance / (maxDisplayDistance /(maxTextSize -minTextSize)));
            transform.LookAt(Camera.main.transform.position);
        }
        else
            mobNameTxt.gameObject.SetActive(false);
    }
}
