using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespownAfterDeath : MonoBehaviour
{
    public bool isDead = false;
    private Entity entity;
    public bool isFading = false;
    public float respownTimer;
    public SkinnedMeshRenderer[] meshRendererArray;

    void Start()
    {
        entity = GetComponent<Entity>();
        meshRendererArray = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        if(isDead)
            StartCoroutine(Respown());
        if (isFading)
            entity.FadeAlpha();
    }
    public IEnumerator Respown()
    {
        isDead = false;
        yield return new WaitForSeconds(3f);
        isFading = true;
        foreach (SkinnedMeshRenderer meshRenderer in meshRendererArray)
            meshRenderer.enabled = false;        
        yield return new WaitForSeconds(respownTimer);
        foreach (SkinnedMeshRenderer meshRenderer in meshRendererArray)
            meshRenderer.enabled = true;
        entity.Respown();        
    }
}
