using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private GameObject floatingTextPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnGoldPopUp(int amount, Vector3 position, float fontSize = 50)
    {
        string textContent = amount > 0 ? $"+{amount}" : $"{amount}";
        SpawnFloatingText(textContent, position, fontSize, Color.yellow);
    }

    public void SpawnDamagePopUp(int amount, Vector3 position, float fontSize = 50)
    {
        string textContent = $"{amount}!";
        SpawnFloatingText(textContent, position, fontSize, Color.red);
    }

    private void SpawnFloatingText(string content, Vector3 position, float fontSize, Color color)
    {
        GameObject prefab = Instantiate(floatingTextPrefab, position, Quaternion.identity);

        TextMeshProUGUI floatingText = prefab.GetComponentInChildren<TextMeshProUGUI>();
        Image image = prefab.GetComponentInChildren<Image>();

        floatingText.text = content;
        floatingText.fontSize = fontSize;
        floatingText.color = color;

        if (image != null) Destroy(image);

        FloatingTextAnimate ft = prefab.AddComponent<FloatingTextAnimate>();
        ft.StartAnimation();

    }

    private class FloatingTextAnimate : MonoBehaviour
    {
        public float moveDistance = 1f;   // Yukarý hareket mesafesi
        public float duration = 1f;       // Animasyon süresi

        private TextMeshProUGUI text;

        public void StartAnimation()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + Vector3.up * moveDistance;
            float elapsed = 0f;
            Color startColor = text.color;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                // Yukarý hareket
                transform.position = Vector3.Lerp(startPos, endPos, t);

                // Fade out
                Color c = startColor;
                c.a = Mathf.Lerp(1f, 0f, t);
                text.color = c;

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}