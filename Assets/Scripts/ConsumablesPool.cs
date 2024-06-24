using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesPool : MonoBehaviour
{
    [SerializeField] private GameObject medkitPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject slugPrefab;
    [SerializeField] private GameObject energyCellPrefab;

    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> medkitList;
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private List<GameObject> slugList;
    [SerializeField] private List<GameObject> energyCellList;
    void Start()
    {
        AddConsumablesToPool(poolSize);
    }

    private void AddConsumablesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject medKit = Instantiate(medkitPrefab);
            medKit.SetActive(false);
            medkitList.Add(medKit);
            medKit.transform.parent = transform;

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;

            GameObject slug = Instantiate(slugPrefab);
            slug.SetActive(false);
            slugList.Add(slug);
            slug.transform.parent = transform;

            GameObject energyCell = Instantiate(energyCellPrefab);
            energyCell.SetActive(false);
            energyCellList.Add(energyCell);
            energyCell.transform.parent = transform;
        }
    }

    public GameObject RequestMedkit()
    {
        for (int i = 0; i < medkitList.Count; i++)
        {
            if (!medkitList[i].activeSelf)
            {
                medkitList[i].gameObject.SetActive(true);
                return medkitList[i];
            }
        }

        return null;
    }

    public GameObject RequestSlug()
    {
        for (int i = 0; i < slugList.Count; i++)
        {
            if (!slugList[i].activeSelf)
            {
                slugList[i].gameObject.SetActive(true);
                return slugList[i];
            }
        }

        return null;
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].gameObject.SetActive(true);
                return bulletList[i];
            }
        }

        return null;
    }

    public GameObject RequestEnergyCell()
    {
        for (int i = 0; i < energyCellList.Count; i++)
        {
            if (!energyCellList[i].activeSelf)
            {
                energyCellList[i].gameObject.SetActive(true);
                return energyCellList[i];
            }
        }

        return null;
    }

}
