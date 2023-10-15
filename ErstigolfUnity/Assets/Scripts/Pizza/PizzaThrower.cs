using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaThrower : MonoBehaviour
{
    [SerializeField] private Rect soloRect; 
    [SerializeField] private Camera turretCam;
    [SerializeField] private float wideFOV;
    [SerializeField] private float narrowFOV;
    [SerializeField] private float zoomSpeed;
    [Space]
    [SerializeField] private bool turretGlobalRotation;
    [SerializeField] private string turretGlobalRotationKey = "TurretGlobalRotation";
    [SerializeField] private Vector2 wideSensitivity;
    [SerializeField] private Vector2 narrowSensitivity;
    [SerializeField] private float shootSpeed = 200;
    [SerializeField] private float loadTime = 1f;

    [SerializeField] private Transform rotor;
    [SerializeField] private Transform shootTransform;

    [Space]
    [SerializeField]
    private Light reloadIndicatorLight;
    [SerializeField] Color noAmmoColor;
    [SerializeField] Color unloadedColor;
    [SerializeField] Color loadedColor;


    private Vector2 currentLook;

    private ButtonsInput inputs;
    private Vector2 aim;
    private bool narrowAim;
    private void Awake() {
        inputs = new ButtonsInput();
        
        if (PlayerPrefs.HasKey("SoloMode") && PlayerPrefs.GetInt("SoloMode") == 1){
            inputs.Solo.Shoot.performed += x => OnEnter();
            inputs.Solo.Aim.performed += x => aim = x.ReadValue<Vector2>();
            inputs.Solo.Zoom.performed += x => narrowAim = x.ReadValueAsButton();
            Cursor.lockState = CursorLockMode.Locked;
            turretCam.rect = soloRect;
            turretCam.targetDisplay = 0;
        }
        else {
            inputs.Car.Green.performed += x => OnEnter();
            inputs.Car.Aim.performed += x => aim = x.ReadValue<Vector2>();
            inputs.Car.Yellow.performed += x => narrowAim = true;
            inputs.Car.Yellow_Release.performed += x => narrowAim = false;
        }
    }

    private void OnEnable() {
        inputs.Enable();
    }

    private void OnDisable() {
        inputs.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(turretGlobalRotationKey))
        {
            turretGlobalRotation = PlayerPrefs.GetInt(turretGlobalRotationKey) == 1;
        }
        PauseMenu.toggleEvent += x => {if (x) inputs.Disable(); else inputs.Enable();};
    }

    private void Update()
    {
        SetArrow();
        UiManager.instance.UpdateLoadedPizza(pizzas.ToArray());

        if(HasAmmo)
        {
            reloadIndicatorLight.color = unloadedColor;
        } else
        {
            reloadIndicatorLight.color = noAmmoColor;
        }
        if(Loaded)
        {
            reloadIndicatorLight.color = loadedColor;
        }
    }

    private void SetArrow()
    {
        Vector2 sensitivity = wideSensitivity;
        float fov = wideFOV;
        if (narrowAim){
            sensitivity = narrowSensitivity;
            fov = narrowFOV;
        }
        currentLook += aim * sensitivity * Time.deltaTime;
        if (turretCam)
            turretCam.fieldOfView = Mathf.Lerp(fov, turretCam.fieldOfView, Mathf.Pow(0.5f, Time.deltaTime * zoomSpeed));
        currentLook.y = Mathf.Clamp(currentLook.y, -90f, 90f);

        CarAudioManager.instance?.ToggleRotation(aim.x != 0, true);
        CarAudioManager.instance?.ToggleRotation(aim.y != 0, false);

        if(!turretGlobalRotation)
        {
            rotor.localRotation = Quaternion.Euler(-currentLook.y, currentLook.x, 0f);
        }
        else
        {
            rotor.rotation = Quaternion.Euler(-currentLook.y, currentLook.x, 0f);
        }
    }

    private void OnEnter()
    {
        if (Loaded)
        {
            LaunchPizza();
        }
        else
        {
            LoadPizza();
        }
    }

    Queue<Transform> pizzas = new Queue<Transform>();
    Transform loadedPizza;
    Transform nextPizza => pizzas.TryDequeue(out Transform result) ? result : null;
    
    private bool HasAmmo => pizzas.TryPeek(out Transform x);
    private bool Loaded => loadedPizza;
    private bool loading;

    public void AddPizzaAmmo(GameObject p) 
    {
        if (p.TryGetComponent(out Collider c)) c.enabled = false;
        if (p.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        pizzas.Enqueue(p.transform);
        
        CarAudioManager.instance?.CompletePizza();
    }

    private async void LoadPizza(){
        if (Loaded || loading)
            return;
        if (!HasAmmo){
            AlertSystem.Message("Keine Munition");
            return;
        }
        loading = true;

        CarAudioManager.instance?.LoadPizza();

        Transform nextPizza = pizzas.Dequeue();

        Transform p = nextPizza.parent;
        float loadStartTime = Time.time;
        while (loadStartTime + loadTime > Time.time){
            float t = (Time.time - loadStartTime)/loadTime;
            nextPizza.position = Vector3.Lerp(p.position, shootTransform.position, t*t);
            await System.Threading.Tasks.Task.Yield();
        }
        nextPizza.position = shootTransform.position;
        nextPizza.parent = shootTransform;
        loadedPizza = nextPizza;

        loading = false;

    }

    private void LaunchPizza() 
    {
        CarAudioManager.instance?.FireRailgun();

        Transform pizza = loadedPizza;
        pizza.GetComponent<Pizza>().flyParticles.SetActive(true);
        loadedPizza = null;
        pizza.position = shootTransform.position;
        pizza.rotation = shootTransform.rotation;

        if (!pizza.TryGetComponent(out Rigidbody rb))
        {
            Debug.LogWarning("pizza has no Rigidbody attached?");
            return;
        }
        if (!pizza.TryGetComponent(out Collider c))
        {
            Debug.LogWarning("pizza has no collider attached");
            return;
        }
        c.enabled = true;
        rb.isKinematic = false;
        rb.velocity = pizza.forward * shootSpeed;
        rb.angularVelocity = Vector3.zero;
        Rigidbody carRB = rotor.parent.GetComponentInParent<Rigidbody>();
        if (carRB){
            rb.velocity += carRB.velocity;
            carRB.AddForceAtPosition(-rb.velocity * rb.mass, shootTransform.position, ForceMode.Impulse);
        }
        pizza.parent = null;

    }
}
