using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudioManager : MonoBehaviour
{
    #region instance
    public static CarAudioManager instance;
    private void Awake()
    {
        if(instance)
        {
            Debug.LogError("More than one CarAudioManager!");
            Destroy(this);
            return;
        }
        instance = this;
    }
    #endregion
    Rigidbody rb;

    [SerializeField] AudioSource engineAudioSource;
    [SerializeField] AnimationCurve pitchCurve;
    [SerializeField] private float maxSpeed;

    public float minPitch = 0.05f;
    private float pitchFromCar;

    [Space]
    [SerializeField] private AudioSource soundBarrierSource;
    [SerializeField] private ParticleSystem soundBarrierParticleSystem;
    [SerializeField] GolfkartSpeed golfkartSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //engineAudioSource = GetComponent<AudioSource>();
        engineAudioSource.pitch = minPitch;

        rb = GetComponent<Rigidbody>();
    }

    bool hasBrokenSoundBarrier = false;
    // Update is called once per frame
    void Update()
    {
        float carSpeed = golfkartSpeed.GetAvgSpeedOverTimeRecord();// rb.velocity.magnitude;
        if(carSpeed > 343)
        {
            if(!hasBrokenSoundBarrier)
            {
                hasBrokenSoundBarrier = true;
                soundBarrierSource.Play();
                soundBarrierParticleSystem?.Play();

            }
        } else 
        {
            hasBrokenSoundBarrier = false;
        }

        carSpeed = rb.velocity.magnitude;
        pitchFromCar = pitchCurve.Evaluate(Mathf.Clamp01(carSpeed / maxSpeed));
        if (pitchFromCar < minPitch)
            engineAudioSource.pitch = minPitch;
        else
            engineAudioSource.pitch = pitchFromCar;
    }



    //
    [Space]
    #region Impact

    [SerializeField] private AudioSource impactAudioSource;
    //[SerializeField] private AudioClip[] impactAudioClips;
    [SerializeField] private float startT = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        playImpact();
    }

    private void playImpact()
    {
        if (impactAudioSource.isPlaying) return;
        //impactAudioSource.clip = impactAudioClips[Random.Range(0, impactAudioClips.Length)];
        impactAudioSource.time = startT;
        impactAudioSource.Play();

    }
# endregion

    #region Flying



    [SerializeField] private AudioSource flyingSource;
    public void ToggleFlying(bool fly)
    {
        if(fly)
        {
            flyingSource.Play();
        } else
        {
            flyingSource.Stop();
        }
    }

    #endregion



    #region Turret
    [Header("Turret")]
    [SerializeField] private AudioSource rotationXSource;
    [SerializeField] private AudioSource rotationYSource;
   public void ToggleRotation(bool on, bool isX)
    {
        var s = (isX ? rotationXSource : rotationYSource);
        if(on)
        {
            s.Play();
        } else
        {
            s.Stop();
        }
    }
    #endregion
    #region TurretActions

    [SerializeField] private AudioSource pizzaCompletedSource;
    [SerializeField] private AudioSource pizzaLoadedSource;
    [SerializeField] private AudioSource railgunfiredSource;

    public void LoadPizza()
    {
        pizzaLoadedSource.Play();
    }
    public void CompletePizza()
    {
        pizzaCompletedSource.Play();
    }
    public void FireRailgun()
    {
        railgunfiredSource.Play();
    }
    #endregion
}
