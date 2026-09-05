using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerSkillManager skillManager { get; private set; }
    public PlayerLaneMovement movement { get; private set; }
    public PlayerInputMobile input { get; private set; }
    public Player_Health health { get; private set; }
    public Player_Effect effect { get; private set; }
    public Player_Combat combat { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_DeadState deadState { get; private set; }

    [Header("Magnet")]
    [SerializeField] private float magnetRadius;
    [SerializeField] private Sprite magnetIcon;
    private bool isMagnetic;
    private Coroutine magnetCo;


    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<PlayerSkillManager>();
        movement = GetComponent<PlayerLaneMovement>();
        input = GetComponent<PlayerInputMobile>();
        health = GetComponent<Player_Health>();
        effect = GetComponent<Player_Effect>();
        combat = GetComponent<Player_Combat>();

        UI.Instance.SetPlayer(this);
    }

    protected override void Start()
    {
        idleState = new Player_IdleState(this, stateMachine, projectile, "Idle");
        runState = new Player_RunState(this, stateMachine, projectile, "Run");
        deadState = new Player_DeadState(this, stateMachine, projectile, "Dead");


        stateMachine.Initialize(idleState);
    }

    protected override void OnEnable()
    {
        if (characterData != null)
            speed = characterData.speed;
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();

        if (isMagnetic)
            GetCoins();
    }

    #region Use Magnet
    public void UseMagnet(float duration)
    {
        if (magnetCo != null)
            StopCoroutine(magnetCo);

        magnetCo = StartCoroutine(UseMagnetCo(duration));
    }

    private IEnumerator UseMagnetCo(float duration)
    {
        isMagnetic = true;
        UI.Instance.IngameUI.IconBarUI.AddOrRefreshEffect("Magnet", magnetIcon, duration, this);
        yield return new WaitForSeconds(duration);
        isMagnetic = false;
    }

    public bool IsMagnetic() => isMagnetic;

    private void GetCoins()
    {
        foreach (var item in FindCoin())
        {
            var coin = item.GetComponent<Coin_Item>();
            if (!coin) continue;

            coin.Pickup(this);
        }
    }
    #endregion

    private Collider2D[] FindCoin()
    {
        return Physics2D.OverlapCircleAll(transform.position, magnetRadius);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
