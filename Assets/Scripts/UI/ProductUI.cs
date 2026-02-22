using TMPro;
using UnityEngine;

public class ProductUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI productText;

    private void Start()
    {
        productText.text = $"Current Resource = {GameDatabase.Instance.currentProduct.productName}"; 
    }
}
