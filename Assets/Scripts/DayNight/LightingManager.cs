using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    //Variables
    [SerializeField, Range(0, 24)] public static float TimeOfDay;
    [SerializeField] public static int Day = 1;
    [SerializeField] private bool stopTime = false; // Added boolean to stop time

    private int lastHour = -1; // Store the last hour to track the change

    public int timeDivider;

    // Public variable for starting time
    [SerializeField, Range(0, 24)] private float startHour = 0;

    private void Start()
    {
        TimeOfDay = startHour; // Set the starting time
    }

    

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying && !stopTime)
        {
            float previousTimeOfDay = TimeOfDay;

            TimeOfDay += Time.deltaTime / (timeDivider);
            TimeOfDay %= 24;

            int currentHour = Mathf.FloorToInt(TimeOfDay);

            if (currentHour != lastHour && currentHour == 0)
            {
                Day++; // Increment Day whenever hour changes to 0
            }

            lastHour = currentHour;

            if (previousTimeOfDay != TimeOfDay)
            {
                UpdateLighting(TimeOfDay / 24f);
            }
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }



    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    // Additional public methods to control time and day
    public void StopTime()
    {
        stopTime = true;
    }

    public void StartTime()
    {
        stopTime = false;
    }

    public void IncrementHours(int hourIncrement)
    {
        TimeOfDay += hourIncrement;
        TimeOfDay %= 24;
        UpdateLighting(TimeOfDay / 24f);
    }

    public void IncrementDays(int days)
    {
        Day += days;
    }
}