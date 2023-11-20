using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] private StoragePanelView storagePanelView;
    [SerializeField] private WarehousePanelView warehousePanelView;
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
    private void Start()
    {
        SetPanelsPositionData();
  
        storagePanelView.OpenWarehouseButton.Button.onClick.AddListener(() => 
        {
            storagePanelView.MovePanel();
            warehousePanelView.MovePanel();
        });
        warehousePanelView.CloseButton.Button.onClick.AddListener(() =>
        {
            storagePanelView.MovePanel();
            warehousePanelView.MovePanel();
        });
    }

    private void SetPanelsPositionData()
    {
        storagePanelView.EndPoint = endPoint;
        storagePanelView.StartPoint = startPoint;
        
        warehousePanelView.EndPoint = endPoint;
        warehousePanelView.StartPoint = startPoint;
        
        storagePanelView.transform.position = endPoint.position;
        warehousePanelView.transform.position = startPoint.position;
    }
}
