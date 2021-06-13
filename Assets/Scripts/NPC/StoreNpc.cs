using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNpc : NPC
{
    public List<Item> itemsToSell;
    public string storeText;
    public Store store;
    public string storeType;
    private AudioSource audioSource;
    public override void Start()
    {
        base.Start();
        store = GameObject.Find("Store").GetComponent<Store>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Interact()
    {
        if (store.isOpen)
            return;
        store.SetTitle(storeType);
        store.OpenStore();
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
