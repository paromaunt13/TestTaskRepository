using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    // [SerializeField] private Vector2 cellSize1;
    // [SerializeField] private Vector2 cellSize2;
    // [SerializeField] private Vector2 cellSize3;

    private IEnumerator ShowCloud(GameObject cloud, float timeToWait, AudioClip appearSound, AudioClip disappearSound)
    {
        cloud.SetActive(true);
        AudioManager.Instance.PlaySound(appearSound);

        yield return new WaitForSeconds(timeToWait);

        AudioManager.Instance.PlaySound(disappearSound);
        cloud.SetActive(false);
    }

    protected void SetCloudView(GameObject cloud, float timeToWait, AudioClip appearSound, AudioClip disappearSound)
    {
        StartCoroutine(ShowCloud(cloud, timeToWait, appearSound, disappearSound));
    }

    // protected void ResizeCells(int productCount, GridLayoutGroup gridLayoutGroup)
    // {
    //     Vector2 newCellSize;
    //
    //     switch (productCount)
    //     {
    //         case 1:
    //             newCellSize = cellSize1;
    //             gridLayoutGroup.cellSize = newCellSize;
    //             break;
    //         case 2:
    //             newCellSize = cellSize2;
    //             gridLayoutGroup.cellSize = newCellSize;
    //             break;
    //         case 3:
    //             newCellSize = cellSize3;
    //             gridLayoutGroup.cellSize = newCellSize;
    //             break;
    //     }
    // }

    protected void Clear(Transform contentParent, List<ProductItemView> productItemViews)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        productItemViews?.Clear();
    }
}