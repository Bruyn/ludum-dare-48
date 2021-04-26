using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public static HudController Instance;

    [SerializeField] private TMP_Text ammoCountText;
    [SerializeField] private Image kickStatusImage;
    [SerializeField] private Image dashStatusImage;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Something went horribly wrong :(");
            return;
        }
        Instance = this;
    }

    public void SetAmmoCount(int currentAmmo, int maxAmmo)
    {
        ammoCountText.text = currentAmmo + "/" + maxAmmo;
    }

    public void UpdateKickStatus(bool kickReady)
    {
        float targetAlpha = kickReady ? 1 : 0.5f;
        kickStatusImage.CrossFadeAlpha(targetAlpha, 0.2f, false);
    }
    
    public void UpdateDashStatus(bool kickReady)
    {
        float targetAlpha = kickReady ? 1 : 0.5f;
        dashStatusImage.CrossFadeAlpha(targetAlpha, 0.2f, false);
    }
}
