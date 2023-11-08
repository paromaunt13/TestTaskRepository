using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    private IEnumerator ShowCloud(GameObject cloud, float timeToWait)
    {
        cloud.SetActive(true);
        AudioManager.Instance.PlaySound(SoundType.BubbleAppearSound);
        yield return new WaitForSeconds(timeToWait);
        AudioManager.Instance.PlaySound(SoundType.BubbleDisappearSound);
        cloud.SetActive(false);
    }

    protected void SetCloudView(GameObject cloud, float timeToWait)
    {
        StartCoroutine(ShowCloud(cloud, timeToWait));
    }

    protected void Clear(Transform contentParent, List<ProductItemView> productItemViews)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        productItemViews?.Clear();
    }
}