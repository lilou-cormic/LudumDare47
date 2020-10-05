using PurpleCable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonBlock : MonoBehaviour
{
    [SerializeField] Season season;
    public Season Season => season;

    [SerializeField] Music Music = null;

    private List<Animal> _animals = new List<Animal>();

    public bool CanAddAnimal => _animals.Count < GameManager.MaxAnimalsPerSeason;

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal)
    {
        _animals.Remove(animal);
    }

    public void PlayMusic()
    {
        if (Music != null)
            Music.Play();
    }
}
