using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] private StoragePanelView storagePanelView;
    [SerializeField] private WarehousePanelView warehousePanelView;

    private void Start()
    {
        SetPanelsPositionData();
  
        storagePanelView.OpenWarehouseButton.Button.onClick.AddListener(() => 
        {
            storagePanelView.MovePanel(storagePanelView.StartPoint, storagePanelView.CanvasGroup);
            warehousePanelView.MovePanel(warehousePanelView.EndPoint, warehousePanelView.CanvasGroup);
        });
        warehousePanelView.CloseButton.Button.onClick.AddListener(() =>
        {
            storagePanelView.MovePanel(storagePanelView.EndPoint, storagePanelView.CanvasGroup);
            warehousePanelView.MovePanel(warehousePanelView.StartPoint,  warehousePanelView.CanvasGroup);
        });
    }

    private void SetPanelsPositionData()
    {
        storagePanelView.transform.position =  storagePanelView.EndPoint.position;
        warehousePanelView.transform.position = warehousePanelView.StartPoint.position;
    }
}
