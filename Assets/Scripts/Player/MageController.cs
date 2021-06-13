using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MageController : MonoBehaviour
{
    [Header("MOVEMENT")]
    public float speed;
    public float rotationSpeed = .1f;
    private Vector3 newRotation;
    public Transform cinemachine;
    public Transform cameraTarget;
    private float horizontalMovement;
    private float verticalMovement;
    private Rigidbody rb;
    public Animator anim;
    private bool isMoving = false;
    public bool canMove = true;
    public bool canRotate = true;
    private GameMaster gameMaster;

    [Space]
    [Header("ABILITY")]
    public Ability[] playerAbilities;
    public Ability castedAbility = null;
    public GameObject particle;
    public Image aim;
    public GameObject aoeMarker;
    public GameObject aoeMarker2;
    public Vector2 aimOffset;
    public PlayerStats playerStats;
    public Transform firePoint;
    public float fireRate = .25f;
    private float fireCD = 0f;
    private bool isRolling = false;
    public Transform target;
    public List<Transform> mobsAround = new List<Transform>();
    public LayerMask enemyLayer;
    public LayerMask aoeLayer;
    public bool canCast = true;   
    
    [Space]
    [Header("EFFECT PREFABS")]
    public GameObject[] effects;
    public GameObject wings;
    private bool isDeathTimerOver = false;
    private PlayEffect playEffect;
    public AudioClip potionDrinkingSound;
    public AudioClip itemEquippedSound;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameMaster.instance;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        gameMaster.OnPlayerLevelUpCallBack += LevelUp;
        effects[3].SetActive(true);
        effects[3].GetComponent<ParticleSystem>().Play();
        playEffect = GetComponentInChildren<PlayEffect>();
    }
    // Update is called once per frame
    void Update()
    {
        fireCD -= Time.deltaTime;
        isMoving = !(horizontalMovement == 0 && verticalMovement == 0);
        if (!isRolling)
            StartCoroutine(Roll());
        AimTarget();
        MobsAroundPlayer();
        BasicAttack();
        AoeAttack();
        Die();
        if(isDeathTimerOver)
            StartCoroutine(Revive());
    }
    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }
    private void Movement()
    {
        if (!canMove)
        {
            horizontalMovement = 0f;
            verticalMovement = 0f;
            anim.SetFloat("InputZ", verticalMovement);
            anim.SetFloat("InputX", horizontalMovement);
            return;
        }            
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        anim.SetFloat("InputZ", verticalMovement);
        anim.SetFloat("InputX", horizontalMovement);
        Vector3 velocity =(horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
        velocity.y = rb.velocity.y;
        if (verticalMovement < 0)
            velocity /= 1.5f;
        velocity *= speed;
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
    void Rotation()
    {
        if (!canRotate)
            return;
        newRotation.y = Input.GetAxis("Mouse Y");
        newRotation.x = Input.GetAxis("Mouse X");
        cameraTarget.rotation *= Quaternion.AngleAxis(newRotation.x * rotationSpeed * Time.fixedDeltaTime, Vector3.up);
        cameraTarget.rotation *= Quaternion.AngleAxis(-newRotation.y * rotationSpeed * Time.fixedDeltaTime, Vector3.right);
        var angles = cameraTarget.localEulerAngles;
        angles.z = 0;
        var angle = cameraTarget.localEulerAngles.x;
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
        cameraTarget.localEulerAngles = angles;
        if (isMoving)
        {
            rb.rotation = Quaternion.Euler(0, cameraTarget.transform.rotation.eulerAngles.y, 0);
            cameraTarget.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
    private IEnumerator Roll()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && verticalMovement > 0)
        {
            isRolling = true;
            anim.SetTrigger("Roll");
            playerStats.isInvulnerable = true;
            float duration = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(duration + 0.5f);
            playerStats.isInvulnerable = false;
            isRolling = false;
        }        
    }
    void AimTarget()
    {
        if (mobsAround.Count != 0)
            target = mobsAround[targetIndex()];
        Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
        if (target == null || !aim.IsActive())
        {
            aim.transform.position = Vector3.MoveTowards(aim.transform.position, screenCenter, Time.deltaTime * 10000);
            aim.transform.rotation = Quaternion.identity;
            return;
        }            
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + new Vector3(0f, target.localScale.y, 0f));
        Vector3 CornerDistance = screenPos - screenCenter;
        Vector3 absCornerDistance = new Vector3(Mathf.Abs(CornerDistance.x), Mathf.Abs(CornerDistance.y), Mathf.Abs(CornerDistance.z));

        if (absCornerDistance.x < screenCenter.x / 3 && absCornerDistance.y < screenCenter.y / 3 && screenPos.x > 0 && screenPos.y > 0 && screenPos.z > 0
    && !Physics.Linecast(transform.position + (Vector3)aimOffset, target.position + (Vector3)aimOffset * 2, enemyLayer))
        {
            aim.transform.position = Vector3.Lerp(aim.transform.position, Vector3.MoveTowards(aim.transform.position, screenPos, Time.deltaTime * 5000), 0.5f);
        }
        else
        {
            aim.transform.position = Vector3.MoveTowards(aim.transform.position, screenCenter, Time.deltaTime * 10000);
            target = null;
        }
    }
    void BasicAttack()
    {
        if (Input.GetMouseButtonDown(0) && canCast && target != null && fireCD <= 0)
        {
            effects[0].GetComponent<ParticleSystem>().Play();
            GameObject go = Instantiate(particle, firePoint.position, firePoint.rotation);
            go.GetComponent<TargetParticle>().target = target;
            fireCD = 0;
            fireCD += fireRate;
            gameMaster.CameraShake(1f, .1f);
        }
    }
    void AoeAttack()
    {   if(aoeMarker.activeSelf)
        {
            var forwardCamera = Camera.main.transform.forward;
            forwardCamera.y = 0.0f;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, aoeLayer))
                aoeMarker.transform.position = new Vector3(hit.point.x, hit.point.y + .1f, hit.point.z);
            if(Input.GetMouseButtonDown(0))
            {
                StartCoroutine(castedAbility.AoeCast(this));
                gameMaster.MouseLock();
            }
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                aoeMarker.SetActive(false);
                aim.enabled = true;
                gameMaster.MouseLock();
            }
        }        
    }
    void MobsAroundPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, enemyLayer);
        foreach (var collider in colliders)
        {
            if (collider.transform.parent.GetComponent<Entity>().enabled && !mobsAround.Contains(collider.transform.parent))
            {
                mobsAround.Add(collider.transform.parent);
            }
        }
        for (int i = 0; i < mobsAround.Count; i++)
        {
            if (!mobsAround[i].GetComponent<Entity>().enabled || Vector3.Distance(transform.position, mobsAround[i].position)> 20f )
            {
                if (target == mobsAround[i])
                    target = null;
                mobsAround.Remove(mobsAround[i]);
            }             
        }
    }
    public int targetIndex()
    {
        float[] distances = new float[mobsAround.Count];
        for (int i = 0; i < mobsAround.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(mobsAround[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }
        float minDistance = Mathf.Min(distances);
        int index = 0;
        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }
    public void LevelUp()
    {
        gameMaster.levelUpPanel.SetActive(true);
        gameMaster.levelUpPanel.GetComponent<Animator>().Play("LevelUp");
        for (int i = 0; i < 2; i++)
        {
            wings.transform.GetChild(i).GetComponent<Animator>().Play("Wings");
        }
        effects[1].SetActive(true);
        effects[2].SetActive(true);
        effects[1].GetComponent<ParticleSystem>().Play();
        effects[2].GetComponent<ParticleSystem>().Play();
    }
    private void Die()
    {
        if (playerStats.currentHealth > 0)
            return;
        anim.SetTrigger("Die");
        gameMaster.CameraShake(2f, .1f);
        this.enabled = false;
        gameMaster.playerDiedPanel.SetActive(true);
        gameMaster.playerDiedPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameMaster.MouseUnlock();
    }    
    private IEnumerator Revive()
    {    
        yield return new WaitForSeconds(2.51f);
        canMove = true;
        canCast = true;
        isDeathTimerOver = false;
    }
    public void CallBackRevive()
    {
        this.enabled = true;
        effects[3].SetActive(true);
        effects[3].GetComponent<ParticleSystem>().Play();
        isDeathTimerOver = true;
        anim.SetTrigger("Revive");
        gameMaster.MouseLock();
        canMove = false;
        canCast = false;
        gameMaster.playerDiedPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameMaster.playerDiedPanel.SetActive(false);
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentMana = playerStats.maxMana;
        transform.position = new Vector3(515f, 10f, 475f);
    }
    public void PlayerHit()
    {
        if(!isRolling)
        {
            effects[4].SetActive(true);
            effects[4].GetComponent<ParticleSystem>().Play();
        }
    }
    public void PlaySoundEffect(AudioClip audioClip)
    {
        playEffect.PlaySoundEffect(audioClip);
    }
}
