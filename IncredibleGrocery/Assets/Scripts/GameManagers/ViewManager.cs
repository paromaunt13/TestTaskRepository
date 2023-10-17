using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;

    [SerializeField] private Vector2 _cellSize1;
    [SerializeField] private Vector2 _cellSize2;
    [SerializeField] private Vector2 _cellSize3;

    private void Awake()
    {
        Instance = this;  
    }

    private IEnumerator ShowCloud(GameObject cloud, float timeToWait, AudioClip appearSound, AudioClip disappearSound)
    {
        cloud.SetActive(true);
        AudioManager.Instance.PlaySound(appearSound);

        yield return new WaitForSeconds(timeToWait);

        AudioManager.Instance.PlaySound(disappearSound);
        cloud.SetActive(false);
    }

    public void SetCloudView(GameObject cloud, float timeToWait, AudioClip appearSound, AudioClip disappearSound)
    {
        StartCoroutine(ShowCloud(cloud, timeToWait, appearSound, disappearSound));
    }

    public void Resize(int productCount, GridLayoutGroup gridLayoutGroup)
    {
        Vector2 newCellSize;

        switch (productCount)
        {
            case 1:
                newCellSize = _cellSize1;
                gridLayoutGroup.cellSize = newCellSize;
                break;
            case 2:
                newCellSize = _cellSize2;
                gridLayoutGroup.cellSize = newCellSize;
                break;
            case 3:
                newCellSize = _cellSize3;
                gridLayoutGroup.cellSize = newCellSize;
                break;
        }
    }

    public void Clear(Transform contentParent, List<ProductItemView> productItemViews)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        productItemViews?.Clear();
    }
}