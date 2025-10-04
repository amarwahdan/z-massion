using UnityEngine;
using UnityEngine.UI;
using TMPro; // لو بتستخدم TextMeshPro

public class BuoyancySystemUI : MonoBehaviour
{
    [Header("Mass Settings (kg)")]
    public float playerMass = 0f;
    public float suitMass = 136f;
    public float buoyantMass = 266f;

    private float addedWeight = 0f;
    private float addedFloaty = 0f;

    [Header("UI Elements")]
    public TMP_InputField playerMassInput;   // Input Field للوزن
    public TextMeshProUGUI statusText;       // Text لعرض النتيجة
    public GameObject setupUI;               // Panel البداية (فيه InputField)
    public GameObject poolUI;                // Panel المسبح (الأزرار)

    void Start()
    {
        // لما اللاعب يكتب ويدوس Enter
        playerMassInput.onSubmit.AddListener(OnEnterPressed);

        // في البداية المسبح مختفي
        poolUI.SetActive(false);
    }

    void OnEnterPressed(string value)
    {
        if (float.TryParse(value, out playerMass))
        {
            float totalMass = playerMass + suitMass;

            statusText.text = $"Player: {playerMass}kg + Suit: {suitMass}kg = {totalMass}kg\n" +
                              "Entering Neutral Buoyancy Lab...";

            // إخفاء شاشة الإدخال
            setupUI.SetActive(false);

            // إظهار واجهة المسبح على طول
            poolUI.SetActive(true);

            UpdateStatus();
        }
        else
        {
            statusText.text = "❌ Please enter a valid number!";
        }
    }

    // أزرار التحكم
    public void AddWeight() { addedWeight += 5f; UpdateStatus(); }
    public void RemoveWeight() { addedWeight -= 5f; UpdateStatus(); }
    public void AddFloaty() { addedFloaty += 5f; UpdateStatus(); }
    public void RemoveFloaty() { addedFloaty -= 5f; UpdateStatus(); }

    // تحديث الحالة
    void UpdateStatus()
    {
        float totalMass = playerMass + suitMass + addedWeight;
        float effectiveBuoyancy = buoyantMass + addedFloaty;
        float net = effectiveBuoyancy - totalMass;

        string status = "🎯 Neutral Buoyancy!";
        if (net > 0.5f) status = "Floating ↑ (add more weight)";
        else if (net < -0.5f) status = "Sinking ↓ (add more floaty)";

        statusText.text =
            $"Mass: {totalMass}kg\n" +
            $"Buoyancy: {effectiveBuoyancy}kg\n" +
            $"Net: {net:F1}kg → {status}";
    }
}
