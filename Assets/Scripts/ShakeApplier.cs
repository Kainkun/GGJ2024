using UnityEngine;

public class ShakeApplier : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform sourceTransform;
    [SerializeField] private float recoverySpeed;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    
    public float Strength { get; set; }

    private void Update()
    {
        // todo: shake sucks
        float time = Time.time * frequency;
        float offsetX = (2 * Mathf.PerlinNoise(0, time)) / 0.5f;
        float offsetY = (2 * Mathf.PerlinNoise(time, 0)) / 0.5f;
        float offsetZ = (2 * Mathf.PerlinNoise(time, time)) / 0.5f;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ) * amplitude;
        offset *= Strength;
        targetTransform.position = sourceTransform.position + offset;
        Strength -= recoverySpeed * Time.deltaTime;
        Strength = Mathf.Clamp01(Strength);
    }
}
