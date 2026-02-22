using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProductData")]
public class ProductData : ScriptableObject
{
    public string productName;
    public Sprite productSprite;
    public int productSellValue;
}
