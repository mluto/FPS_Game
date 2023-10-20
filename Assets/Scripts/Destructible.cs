using UnityEngine;
using UnityEngine.UI;

public abstract class Destructible : MonoBehaviour
{
    private enum ObjectMaterial
    {
        Glass,
        Electronic,
        Steel
    }

    [SerializeField] private Slider sliderHp;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] [Tooltip("Object starting HP")] private int startHp = 100;
    [SerializeField] [Tooltip("The material from which the object is made")] private ObjectMaterial material;
    [SerializeField] [Tooltip("All elements of the object")] private GameObject[] elements;
    [SerializeField] [Tooltip("All particles that are fired upon destruction")] private ParticleSystem[] particleEffects;

    private int currentHp;


    private void Start()
    {
        currentHp = startHp;
        sliderHp.maxValue = currentHp;
        sliderHp.value = currentHp;
    }

    private void Update()
    {
        sliderHp.transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// Private method that compares the type of weapon and the material of the destructible object.
    /// </summary>
    private bool IsGoodMaterial(int weaponNumber)
    {
        bool correct = false;

        switch (weaponNumber)
        {
            case 0:
                //green
                if ((int)material == 0 || (int)material == 1) correct = true;
                break;

            case 1:
                //orange
                if ((int)material == 0 || (int)material == 2) correct = true;
                break;

            case 2:
                //purple
                if ((int)material == 1 || (int)material == 2) correct = true;
                break;

            default:
                break;
        }

        return correct;
    }

    /// <summary>
    /// Protected abstract method that is executed after the object is destroyed.
    /// </summary>
    protected abstract void AfterKill();

    /// <summary>
    /// Public function that is executed when object is hited.
    /// </summary>
    public void AddDamage(int damage, int weaponNumber)
    {
        if (currentHp > 0 && IsGoodMaterial(weaponNumber))
        {
            currentHp -= damage;

            if (currentHp <= 0)
            {
                Killed();
            }

            sliderHp.value = currentHp;
        }
    }

    /// <summary>
    /// Private method that destroyed object when it lost all hp.
    /// </summary>
    private void Killed()
    {
        audioSource.Play();
        sliderHp.gameObject.SetActive(false);

        foreach (GameObject go in elements)
        {
            go.SetActive(false);
            Destroy(go, 10f);
        }

        foreach (ParticleSystem particle in particleEffects)
        {
            var newParticle = Instantiate(particle);
            newParticle.transform.position = elements[0].transform.position;
            newParticle.transform.parent = transform;
        }

        AfterKill();

        Destroy(gameObject, 10f);
    }
}
