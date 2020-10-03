using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    private SeasonBlock SeasonBlock;

    private void Awake()
    {
        SeasonBlock = GetComponentInParent<SeasonBlock>();

        if (SeasonBlock == null)
            Destroy(gameObject);

        InvokeRepeating("Spawn", 0f, 5f);
    }

    private void Spawn()
    {
        if (SeasonBlock.Season == GameManager.CurrentSeason)
            return;

        if (!SeasonBlock.CanAddAnimal)
            return;

        Animal animal = Instantiate(GameManager.AnimalPrefab, GetRandomPosition(), Quaternion.identity, SeasonBlock.transform);
        animal.SetDef(Animals.GetRandomAnimal());

        SeasonBlock.AddAnimal(animal);
    }

    private Vector3 GetRandomPosition()
    {
        return GameManager.GetClampedPosition(new Vector3(Random.Range(0f, GameManager.ScreenWidth) - GameManager.ScreenMaxCoord.x,
            Random.Range(0f, GameManager.ScreenHeight) - GameManager.ScreenMaxCoord.y));
    }
}
