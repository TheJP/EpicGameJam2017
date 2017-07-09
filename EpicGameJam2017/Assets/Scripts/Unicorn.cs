using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class Unicorn : MonoBehaviour, PlayerMarkable
{
    [Tooltip("Player which controls this unicorn")]
    public Players player;

    [Tooltip("Wheter the unicorn is currently switched on or not (can be changed by the player with the Switch key)")]
    public bool controlsActive = false;

    [Tooltip("Unicorn turnspeed in degrees per second")]
    public float turnSpeed = 1f;

    /// <summary>Don't change this value at runtime. <see cref="SpeedForce"/> should be used for that.</summary>
    [Tooltip("Force which is added when the action key is pressed")]
    public float speedForce = 10f;

    [Tooltip("How much the unicorn dirfts")]
    public float driftFactor = 0.25f;

    [Tooltip("Marks the tip of the green horn")]
    public Transform marker;

    [Tooltip("Box that stores the unicorn ability until it is used")]
    public AbilityHolder abilityHolder;

    [Tooltip("Prefab of the toothpick which is used in the toothpick ability")]
    public Toothpick toothpickPrefab;

    [Tooltip("Prefab for the nimbus 3000 broom stick")]
    public Nimbus3000Decoration nimbus3000Prefab;

    [Tooltip("Amount of seconds that this unicorn is stunned (e.g. if it is hit by a toothpick)")]
    public float stunDuration = 1f;

    [Tooltip("MeshRenderer that renders the unicorn")]
    public MeshRenderer unicornRenderer;

    [Tooltip("One of these sounds is played if the unicorn is hurt")]
    public AudioClip[] hurtSounds;

    [Tooltip("One of these sounds is played if the unicorn is laughing")]
    public AudioClip[] laughingSounds;

    [Tooltip("Sound with sentence that the unicorn may say")]
    public AudioClip iLikeTrainsSound;

    [Tooltip("Confused ducks game object that spins around the unicorns head while it is stunned")]
    public GameObject confusedDucks;

    private Ingredient ingredient = null;
    private Controller controller = null;

    /// <summary>Time, when the unicorn was stunned.</summary>
    private float stunTime;

    /// <summary>If the unicorn is currently cheesed.</summary>
    private bool isCheesed;

    /// <summary>Ingredient, which this unicorn currently carries.</summary>
    public Ingredient CarryIngredient { get { return ingredient; } }
    
    public bool ShouldBeMarked { get { return controlsActive; } }

    public Players Player { get { return player; } }

    public Vector3 Position { get { return transform.position; } }

    /// <summary>
    /// Stores the difference between unicorn and ability holder position,
    /// so the position of the ability holder relative to the unicorn can later be restored
    /// </summary>
    private Vector3 abilityHolderPositionDifference;

    public float SpeedForce { get; set; }

    /// <summary>Flag indicating, if the unicorn is flying. (Flying unicorns can avoid trains and cheese)</summary>
    public bool IsFlying { get; private set; }

    /// <summary>Set the given ingredient to belong to this unicorn.</summary>
    public bool SetIngredient(Ingredient ingredient)
    {
        if (this.ingredient != null) { return false; }
        this.ingredient = ingredient;
        ingredient.SetCollidersActive(false);
        return true;
    }

    public void Awake()
    {
        SpeedForce = speedForce;
        stunTime = -2 * stunDuration; // No stun at the beginning
        controller = FindObjectOfType<Controller>();
        if (controller == null) { throw new System.ArgumentException(); }
        abilityHolderPositionDifference = abilityHolder.transform.position - transform.position;
    }

    private void Start()
    {
        // Set unicorn color
        var materials = unicornRenderer.materials;
        materials[0] = new Material(materials[0]) { color = Constants.PlayerColors[player] };
        unicornRenderer.materials = materials;
    }

    public void Update()
    {
        if (!IsStunned && controlsActive && Input.GetButtonDown(Constants.SpecialButton + player))
        {
            // Place ingredient
            if (ingredient != null)
            {
                if (controller.DropIngredientOnPizza(player, ingredient))
                {
                    ingredient.WaitThenActivateColliders(1f);
                    ingredient = null;
                }
            }
            // Activate powerup (only works if slot is not empty)
            else
            {
                abilityHolder.CastAbility(this);
            }
        }
        // Switch between unicorn and train
        if (Input.GetButtonDown(Constants.SwitchButton + player))
        {
            controlsActive = !controlsActive;

            // Play "I like trains" with 20% probability when switching control
            if (!controlsActive && Random.Range(0f, 100f) <= 20f)
            {
                GetComponent<AudioSource>().PlayOneShot(iLikeTrainsSound);
            }
        }

        // Spin ducks while stunned
        if (IsStunned)
        {
            confusedDucks.transform.Rotate(confusedDucks.transform.forward, 10f);
        }
    }

    public void FixedUpdate()
    {
        if (controlsActive)
        {
            var body = GetComponent<Rigidbody2D>();

            body.velocity = ForwardVelocity() + driftFactor * RightVelocity();

            if (!IsStunned)
            {
                // Turn unicorn
                body.angularVelocity = Input.GetAxis(Constants.HorizontalAxis + player) * turnSpeed;
                // Accelerate unicorn
                if (Input.GetButton(Constants.ActionButton + player))
                {
                    body.AddForce(transform.up * SpeedForce, ForceMode2D.Force);
                }
            }
        }
    }

    private void LateUpdate()
    {
        // Hold the ingredient on the horn
        if (ingredient != null)
        {
            ingredient.transform.position = marker.position;
        }

        // Hold the white box (ability holder) in the right position and rotation
        abilityHolder.transform.position = transform.position + abilityHolderPositionDifference;
        abilityHolder.transform.rotation = Quaternion.identity;
    }

    /// <summary>Forward part of the velocity</summary>
    private Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    /// <summary>Forward part of the velocity</summary>
    private Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    /// <summary>
    /// Instantiates a toothpick and throws it in the direction that this unicorn is facing.
    /// </summary>
    public void ThrowToothpick()
    {
        var toothpick = Instantiate(toothpickPrefab, transform.position, transform.rotation);
        toothpick.GetComponent<Rigidbody2D>().velocity = transform.up;
        toothpick.Thrower = this;
    }

    public bool IsStunned
    {
        get { return Time.time < stunTime + stunDuration; }
    }

    /// <summary>
    /// Render unicorn immobilized for <see cref="stunDuration"/> seconds.
    /// </summary>
    public void Stun()
    {
        stunTime = Time.time;
        PlayHurtSound();
        StartCoroutine(AnimateConfusedDucks());

        // Loose ingredient when stunned
        if (ingredient != null)
        {
            Destroy(ingredient.gameObject);
            ingredient = null;
        }
    }

    private IEnumerator AnimateConfusedDucks()
    {
        confusedDucks.SetActive(true);
        yield return new WaitForSeconds(stunDuration);
        confusedDucks.SetActive(false);
    }

    /// <summary>
    /// Slow down unicorn for 2 seconds.
    /// </summary>
    public void SetCheesed()
    {
        if (isCheesed || IsFlying) { return; }
        isCheesed = true;
        StartCoroutine(SlowBecauseOfCheese());
        PlayHurtSound();
    }

    public void SetFlying(bool isFlying)
    {
        IsFlying = isFlying;
        if (isFlying) { isCheesed = false; }
    }

    private IEnumerator SlowBecauseOfCheese()
    {
        SpeedForce = speedForce * 0.1f; // 10% of normal speed
        yield return new WaitForSeconds(2.0f);
        // Cheesed could be overwritten by flying
        if (isCheesed)
        {
            SpeedForce = speedForce;
            isCheesed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Constants.KnifeTag) { Stun(); }
    }

    /// <summary>
    /// Spawns nimbus for speed ability and returns spawned GameObject.
    /// </summary>
    public Nimbus3000Decoration SpawnNimbus()
    {
        return Instantiate(nimbus3000Prefab, transform);
    }

    public void PlayHurtSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
    }
}
