using StarterAssets;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticles;
    [SerializeField] private AudioSource gunAudio;
    [SerializeField] private Light gunLight;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private MeshRenderer gunMesh;
    [SerializeField] private Animator animator;
    [SerializeField] [Tooltip("The amount of damage dealt")] private int damagePerShot = 40;
    [SerializeField] [Tooltip("Time between bullets")] private float timeBetweenBullets = 0.5f;
    [SerializeField] [Tooltip("Weapon color type")] private Color[] colors;

    private RaycastHit shootHit;
    private int shootableMask;
    private int gunCount = 0;
    private float effectsDisplayTime = 0.2f;
    private float timer;
    private bool colision;

    private void Start()
    {
        shootableMask = LayerMask.NameToLayer("Shootable");
        gunMesh.material.color = colors[gunCount];
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (input.nextWeapon)
        {
            input.nextWeapon = false;

            StartCoroutine(SwitchWeapon());
        }

        if (input.fire && timer >= timeBetweenBullets && !colision)
        {
            input.fire = false;
            timer = 0f;

            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            gunLight.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
        {
            colision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colision = false;
    }

    /// <summary>
    /// Private method for select next weapon.
    /// </summary>
    private IEnumerator SwitchWeapon()
    {
        animator.SetTrigger("GunSwitch");

        if (gunCount < colors.Length - 1)
        {
            gunCount++;
        }
        else
        {
            gunCount = 0;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        gunMesh.material.color = colors[gunCount];
    }

    /// <summary>
    /// Private method of firing a gun
    /// </summary>
    private void Shoot()
    {
        var fwd = transform.TransformDirection(Vector3.forward);

        gunParticles.Stop();
        gunParticles.Play();
        gunAudio.Play(); 
        animator.SetTrigger("GunFire");

        if (Physics.Raycast(transform.position, fwd, out shootHit))
        {
            var go = Instantiate(bulletHole, shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            go.transform.parent = shootHit.collider.gameObject.transform;
            Destroy(go, 5);

            if (shootHit.collider.gameObject.layer == shootableMask)
            {
                go.transform.GetChild(0).gameObject.layer = shootableMask;
                Destructible enemyHealth = shootHit.collider.GetComponentInParent<Destructible>();

                if (enemyHealth != null)
                {
                    enemyHealth.AddDamage(damagePerShot, gunCount);
                }
            }
        }
    }

}
