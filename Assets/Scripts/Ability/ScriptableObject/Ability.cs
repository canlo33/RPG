using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/New Ability")]
public class Ability : ScriptableObject
{
    public string abilityName = "New Ability";
    public GameObject abilityPrefab;
    public Sprite icon;
    public AudioClip abilityAudio;
    public int abilityLevel = 1;
    private AudioSource audioSource;
    public string description = "Default Description";
    public string abilityAnimation;
    public float animationStartTimer;
    private float animationEndTimer;
    public int abilityCost = 10;
    public float cdTimer = 5f;
    public bool isInCd = false;
    public bool isAoe = false;
    public bool isProjectile = false;
    public bool isOnTaget = false;
    public float abilityRadius = 7f;
    public float abilityRange = 10f;
    public float particleSpeed = 7f;
    private Transform target;
    //Ability Damage Parameters
    public float baseDamage = 15f;
    public float damageMultiplier = 1.2f;  
    public virtual void Cast(MageController mageController)
    {
        if (isInCd)
            return;
        if (isAoe)
            AoePreCast(mageController);
        else if (isOnTaget)
            mageController.StartCoroutine(OnTargetAbility(mageController));
        else if (isProjectile)
            mageController.StartCoroutine(ProjectileAbility(mageController));          
    }
    public virtual void AoePreCast(MageController mageController)
    {
        if (!CheckCondition(mageController))
            return;
        GameMaster.instance.MouseUnlock();
        Cursor.visible = false;
        mageController.aoeMarker.SetActive(true);
        mageController.aoeMarker.transform.localScale = new Vector3(abilityRadius, abilityRadius, abilityRadius);
        mageController.castedAbility = this;    
    }
    public virtual IEnumerator AoeCast(MageController mageController)
    {
        if(Vector3.Distance(mageController.transform.position, mageController.aoeMarker.transform.position) > abilityRange)
        {
            mageController.aoeMarker.SetActive(false);
            GameMaster.instance.MouseLock();
            mageController.aim.transform.position = new Vector3(Screen.width, Screen.height, 0) / 2;
            yield break;
        }
        mageController.playerStats.currentMana -= abilityCost;
        Vector3 rotation = new Vector3(mageController.aoeMarker.transform.position.x, mageController.transform.position.y, mageController.aoeMarker.transform.position.z);      
        mageController.anim.SetTrigger(abilityAnimation);
        animationEndTimer = mageController.anim.GetCurrentAnimatorClipInfo(0).Length + .5f;
        audioSource = mageController.GetComponent<AudioSource>();
        audioSource.PlayOneShot(abilityAudio);
        mageController.aoeMarker.SetActive(false);

        yield return new WaitForSeconds(animationStartTimer);
        GameObject go = Instantiate(abilityPrefab);
        GameMaster.instance.CameraShake(1f, .5f);
        go.transform.position = mageController.aoeMarker.transform.position;
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        if(ps != null)
        {
            ps.Play();
            Destroy(go, ps.main.duration);
        }
        isInCd = true;

        yield return new WaitForSeconds(animationEndTimer);
        mageController.canMove = true;
        mageController.canRotate = true;
        mageController.canCast = true;
        mageController.aim.enabled = true;
        mageController.aim.transform.position = new Vector3(Screen.width, Screen.height, 0) / 2;

        yield return new WaitForSeconds(cdTimer - animationStartTimer);
        isInCd = false;  
    }
    public virtual IEnumerator OnTargetAbility(MageController mageController)
    {
        if (mageController.target == null || !CheckCondition(mageController))
            yield break;
        target = mageController.target;
        Vector3 rotation = new Vector3(target.position.x, mageController.transform.position.y, target.position.z);
        mageController.transform.LookAt(rotation);
        mageController.playerStats.currentMana -= abilityCost;
        mageController.anim.SetTrigger(abilityAnimation);
        animationEndTimer = mageController.anim.GetCurrentAnimatorClipInfo(0).Length +.25f;
        audioSource = mageController.GetComponent<AudioSource>();
        audioSource.PlayOneShot(abilityAudio);
        mageController.canMove = false;
        mageController.canRotate = false;
        mageController.canCast = false;

        yield return new WaitForSeconds(animationStartTimer);
        Vector3 offset = new Vector3(0f, target.localScale.y, 0f);
        GameObject go = Instantiate(abilityPrefab, target.position + offset, target.rotation);
        go.transform.parent = target.parent;
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        float damageAmount = (mageController.playerStats.currentDamage * damageMultiplier) + baseDamage;
        damageAmount = Random.Range(damageAmount * .85f, damageAmount * 1.15f);
        target.GetComponent<Entity>().healthSystem.Damage((int)damageAmount);
        target.GetComponent<Entity>().DamageTextPopup((int)damageAmount);
        target.GetComponent<Entity>().isEnraged = true;
        Destroy(go, ps.main.duration);
        isInCd = true;

        yield return new WaitForSeconds(animationEndTimer);
        mageController.canMove = true;
        mageController.canRotate = true;
        mageController.canCast = true;

        yield return new WaitForSeconds(cdTimer - animationStartTimer);
        isInCd = false;
    }
    public virtual IEnumerator ProjectileAbility(MageController mageController)
    {
        if (mageController.target == null || !CheckCondition(mageController))
            yield break;
        target = mageController.target;
        mageController.playerStats.currentMana -= abilityCost;
        Vector3 rotation = new Vector3(target.position.x, mageController.transform.position.y, target.position.z);
        mageController.transform.LookAt(rotation);
        mageController.anim.SetTrigger(abilityAnimation);
        animationEndTimer = mageController.anim.GetCurrentAnimatorClipInfo(0).Length + .25f;
        audioSource = mageController.GetComponent<AudioSource>();
        audioSource.PlayOneShot(abilityAudio);
        mageController.canMove = false;
        mageController.canRotate = false;
        mageController.canCast = false;

        yield return new WaitForSeconds(animationStartTimer);
        GameObject go = Instantiate(abilityPrefab, mageController.firePoint.position, mageController.firePoint.rotation);
        go.GetComponent<TargetParticle>().speed = particleSpeed;
        go.GetComponent<TargetParticle>().target = target;
        isInCd = true;

        yield return new WaitForSeconds(animationEndTimer);
        mageController.canMove = true;
        mageController.canRotate = true;
        mageController.canCast = true;

        yield return new WaitForSeconds(cdTimer - animationStartTimer);
        isInCd = false;
    }
    public bool CheckCondition(MageController mageController)
    {
        if (mageController.playerStats.currentMana < abilityCost)
        {
            TooltipManager.instance.ShowToolTip("Not Enough Mana!", new TooltipManager.ToolTipTimer { timer = 2f });
            return false;
        }            
        else return true;
    }
    private void OnEnable()
    {
        isInCd = false;
    }
}
    

