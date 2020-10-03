using System.Linq;
using UnityEngine;

public static class Animals
{
    private static AnimalDef[] _animalDefs;

    public static void Load()
    {
        if (_animalDefs == null)
            _animalDefs = Resources.LoadAll<Sprite>("Animals").Select(x => new AnimalDef(x.name, x)).ToArray();
    }

    public static void SetSeasons()
    {
        int season = Random.Range(0, 4);

        for (int i = 0; i < _animalDefs.Length; i++)
        {
            _animalDefs[i].Season = (Season)((season++) % 4);
        }
    }

    public static AnimalDef GetRandomAnimal()
    {
        return _animalDefs.GetRandom();
    }
}
