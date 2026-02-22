using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public ConveyorData Data { get; private set; }
    [SerializeField] private ConveyorDirection direction;

    enum ConveyorDirection
    {
        Left,
        Right,
    }

    private void Start()
    {
        Data = GameDatabase.Instance.ConveyorData;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject != null)
        {
            Vector3 dir = direction == ConveyorDirection.Left ? Vector3.left : Vector3.right;
            col.transform.position += dir * Data.conveyorSpeed * Time.deltaTime;
        }
    }
}