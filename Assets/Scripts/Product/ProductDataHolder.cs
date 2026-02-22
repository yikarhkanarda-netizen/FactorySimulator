using UnityEngine;

public class ProductDataHolder : MonoBehaviour
{
    public ProductData productData;

    public void Initialize(ProductData data)
    {
        productData = data;

        SetVisual();
    }

    private void SetVisual()
    {
        gameObject.name = productData.productName;
        GetComponent<SpriteRenderer>().sprite = productData.productSprite;
    }
    private void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < -0.1f || viewPos.x > 1.1f ||
            viewPos.y < -0.1f || viewPos.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }
}