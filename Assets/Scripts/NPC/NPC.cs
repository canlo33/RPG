using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPC : MonoBehaviour
{
    public new string name;
    public Sprite npcIcon;
    public string[] Sentences = new string[5];
    private TextMeshPro npcNameText;
    private Transform player;
    public virtual void Start()
    {
        player = GameMaster.instance.player.transform;
        npcNameText = GetComponentInChildren<TextMeshPro>();
    }
    public virtual void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= 10f)
        {
            npcNameText.enabled = false;
            return;
        }
        else
        {
            npcNameText.transform.parent.LookAt(Camera.main.transform.position);
            npcNameText.enabled = true;
        }
    }
    public virtual void Interact()
    {

    }
    private void OnMouseDown()
    {
        if (Vector3.Distance(player.position, transform.position) >= 5f)
            return;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        Interact();
    }
}
