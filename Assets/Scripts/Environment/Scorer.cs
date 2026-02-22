using UnityEngine;

public class Scorer : MonoBehaviour
{
    public ScorerData Data { get; private set; }

    private void Start()
    {
        Data = GameDatabase.Instance.ScorerData;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out ProductDataHolder holder))
            return;

        int price = Mathf.RoundToInt(holder.productData.productSellValue * Data.sellValueMultiplier);

        MoneyManager.Instance.AddMoney(price, other.transform.position);
        Debug.Log($"Scored! +{price}");
        Destroy(other.gameObject, 1f);
    }
}