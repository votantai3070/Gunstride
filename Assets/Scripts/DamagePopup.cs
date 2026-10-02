using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;

    private TextMeshPro tmp;
    private float time;
    private Vector3 origin;
    private Color baseColor;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        time = 0f;
        origin = transform.position;
    }

    public void Setup(string damageText, Color color = default)
    {
        if (tmp == null)
            tmp = GetComponent<TextMeshPro>();

        time = 0f;
        origin = transform.position;
        baseColor = color == default ? tmp.color : color;
        tmp.color = baseColor;

        tmp.text = damageText;
    }

    private void Update()
    {
        float alpha = opacityCurve.Evaluate(time);

        Color currentColor = baseColor;
        currentColor.a = alpha;
        tmp.color = currentColor;

        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        transform.position = origin + new Vector3(0f, 1f + heightCurve.Evaluate(time), 0f);

        time += Time.deltaTime;
    }
}