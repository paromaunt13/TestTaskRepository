using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyersSpawnManager : MonoBehaviour
{
    [SerializeField] private Buyer buyerPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private ExitZone exitZone;

    [Header("Path points")] 
    [SerializeField] private List<Transform> pathPointList;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform entrancePoint;
    
    [SerializeField] private float minTimeToNewBuyer;
    [SerializeField] private float maxTimeToNewBuyer;
    [SerializeField] private int maxBuyersAmount;

    private Vector3[] _pathPoints;
    
    private float _timeToNewBuyer;
    private int _currentBuyersAmount;
    
    private List<Buyer> _buyersQueueList;
    
    private void Start()
    {
        _buyersQueueList = new List<Buyer>();
        exitZone.OnBuyerExit += DestroyBuyer;
        SetPathPoints();
        StartCoroutine(BuyerSpawning());
    }

    private void OnDestroy() => exitZone.OnBuyerExit -= DestroyBuyer;
    private void SetPathPoints()
    {
        _pathPoints = new Vector3[pathPointList.Count];
        for (var i = 0; i < pathPointList.Count; i++)
            _pathPoints[i] = pathPointList[i].transform.position;
    }
    
    private void DestroyBuyer(Buyer buyer)
    {
        if (_buyersQueueList.Count <= 0) return;
        _currentBuyersAmount--;
        _buyersQueueList.Remove(buyer);
        Destroy(buyer.gameObject);
    }

    private IEnumerator BuyerSpawning()
    {
        while (true)
        {
            _timeToNewBuyer = Random.Range(minTimeToNewBuyer, maxTimeToNewBuyer);
            if (_buyersQueueList.Count == 0)
                _timeToNewBuyer = 1;
                
            yield return new WaitForSeconds(_timeToNewBuyer);
            if (_currentBuyersAmount == maxBuyersAmount)
                yield return new WaitUntil(() => _currentBuyersAmount < maxBuyersAmount);
            SetNewBuyer();
        }
    }

    private void SetNewBuyer()
    {
        var buyer = Instantiate(buyerPrefab, spawnPoint.position, Quaternion.identity, spawnParent);
        _buyersQueueList.Add(buyer);
        _currentBuyersAmount++;
        SetBuyerMoveData(buyer);
    }

    private void SetBuyerMoveData(Buyer buyer)
    {
        buyer.PathPoints = _pathPoints;
        buyer.EntrancePoint = entrancePoint;
        buyer.ExitPoint = exitPoint;
    }
}