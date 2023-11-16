using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyersQueueManager : MonoBehaviour
{
    [SerializeField] private Buyer buyerPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private ExitZone exitZone;
    [SerializeField] private OrderZone orderZone;

    [Header("Path points")] 
    [SerializeField] private List<Transform> pathPoints;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform entrancePoint;
    
    [SerializeField] private float minTimeToNewBuyer;
    [SerializeField] private float maxTimeToNewBuyer;
    [SerializeField] private int maxBuyersAmount;
    
    private float _timeToNewBuyer;

    private List<Buyer> _buyersQueueList;
    
    private void Start()
    {
        _buyersQueueList = new List<Buyer>();

        StartCoroutine(SpawnNewBuyer());

        orderZone.OnBuyerLeaved += SetNewBuyer;
        exitZone.OnBuyerExit += DestroyBuyer;
    }
    
    private void OnDestroy()
    {
        orderZone.OnBuyerLeaved -= SetNewBuyer;
        exitZone.OnBuyerExit -= DestroyBuyer;
    }

    private void SetNewBuyer(Buyer buyer)
    {
        if (_buyersQueueList.Count > 0)
        {
            _buyersQueueList.Remove(buyer);
            RepositionBuyersQueue();
        }
        StartCoroutine(SpawnNewBuyer());
    }

    private void DestroyBuyer(Buyer buyer)
    {
        buyer.DOKill();
        buyer.StopAllCoroutines();
        Destroy(buyer.gameObject);
    }
    
    private IEnumerator SpawnNewBuyer()
    {
        while (_buyersQueueList.Count < maxBuyersAmount)
        {
            if (_buyersQueueList.Count == maxBuyersAmount) yield break;
                _timeToNewBuyer = Random.Range(minTimeToNewBuyer, maxTimeToNewBuyer);
            if (_buyersQueueList.Count == 0)
                _timeToNewBuyer = 1f;
            
            yield return new WaitForSeconds(_timeToNewBuyer);
            
            var buyer = Instantiate(buyerPrefab, spawnPoint.position, Quaternion.identity, spawnParent);
            _buyersQueueList.Add(buyer);
            
            SetBuyerMoveData(buyer);
        }
    }

    private void RepositionBuyersQueue()
    {
        foreach (var buyer in _buyersQueueList.Where(buyer => buyer.CurrentIndexInQueue != 0))
        {
            buyer.CurrentIndexInQueue -= 1;
            buyer.MoveInQueue(pathPoints[buyer.CurrentIndexInQueue]);
        }
    }

    private void SetBuyerMoveData(Buyer buyer)
    {
        buyer.OrderPathPoints = pathPoints;
        buyer.EntrancePoint = entrancePoint;
        buyer.ExitPoint = exitPoint;
        buyer.CurrentIndexInQueue = _buyersQueueList.IndexOf(buyer);
    }
}
