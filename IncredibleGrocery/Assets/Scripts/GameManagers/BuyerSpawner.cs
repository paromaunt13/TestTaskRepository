using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BuyerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private ExitZone exitZone;

    [Header("Path points")] 
    [SerializeField] private List<Transform> pathPointList;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform entrancePoint;
    
     private Buyer _buyerPrefab;
     private float _minTimeToNewBuyer;
     private float _maxTimeToNewBuyer;
     private int _maxBuyersAmount;

    private DiContainer _container;
    private Vector3[] _pathPoints;
    
    private float _timeToNewBuyer;
    private int _currentBuyersAmount;
    
    private List<Buyer> _buyersQueueList;

    [Inject]
    private void Construct(BuyerSpawnerConfig buyerSpawnerConfig)
    {
        _buyerPrefab = buyerSpawnerConfig.BuyerPrefab;
        _minTimeToNewBuyer = buyerSpawnerConfig.MinTimeToNewBuyer;
        _maxTimeToNewBuyer = buyerSpawnerConfig.MaxTimeToNewBuyer;
        _maxBuyersAmount = buyerSpawnerConfig.MaxBuyersAmount;
    }
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
            _timeToNewBuyer = Random.Range(_minTimeToNewBuyer, _maxTimeToNewBuyer);
            if (_buyersQueueList.Count == 0)
                _timeToNewBuyer = 1;
                
            yield return new WaitForSeconds(_timeToNewBuyer);
            if (_currentBuyersAmount == _maxBuyersAmount)
                yield return new WaitUntil(() => _currentBuyersAmount < _maxBuyersAmount);
            SpawnNewBuyer();
        }
    }

    private void SpawnNewBuyer()
    {
        var buyer = Instantiate(_buyerPrefab, spawnPoint.position, Quaternion.identity, spawnParent);
        buyer.SetBuyerMoveData(_pathPoints, entrancePoint, exitPoint);
        _buyersQueueList.Add(buyer);
        _currentBuyersAmount++;
    }
}