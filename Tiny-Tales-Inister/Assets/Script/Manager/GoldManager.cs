using BayatGames.SaveGameFree;
using UnityEngine;

public class GoldManager : Singletone<GoldManager>
{
    private readonly string GOLD_KEY = "Golds"; 

    public float Golds { get; private set; }

    private void Start()
    {
        Golds = SaveGame.Load(GOLD_KEY,0f);
    }

    public void AddGolds(float amount)
    {
        Golds += amount;
    }

    public void SpendGolds(float amount)
    {
        Golds -= amount;
    }

    public void SaveGold() => SaveGame.Save(GOLD_KEY, Golds);
    public void LoadGold() => Golds = SaveGame.Load(GOLD_KEY, 0f);
}
