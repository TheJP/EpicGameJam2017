using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    [Tooltip("Play field that is managed by this controller")]
    public HexagonGrid hexagonGrid;

    [Tooltip("Amount of seconds a ingredient takes to respawn after a refresh")]
    public int ingredientCountdownTime = 5;

    [Tooltip("Prefab of the ingredient countdown")]
    public IngredientCountdown countdownPrefab;

    [Tooltip("Unicorn prefab, which will be used to instantiate unicorns for players")]
    public Unicorn unicornPrefab;

    [Tooltip("CannonWaggon (train) prefab, which will be used to instantiate CannonWaggons for players")]
    public GameObject TrainPrefab;

    [Tooltip("Container with whose children's transforms serve as potential Startlocations and orientations for CannonWaggons")]
    public Transform CannonWaggonStartLocations;

    [Tooltip("The UI text where the player score should be kept track of")]
    public Text PlayerScoreView;
  
    private Players[] players;

    public bool DropIngredientOnPizza(Players player, Ingredient ingredient)
    {
        // Check for nearby hexagon tiles
        var minimalDistance = float.PositiveInfinity;
        Transform closest = null;
        for (int i = hexagonGrid.transform.childCount - 1; i >= 0; --i)
        {
            var child = hexagonGrid.transform.GetChild(i);
            var distance = Vector2.SqrMagnitude(child.position - ingredient.transform.position);
            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                closest = child;
            }
        }

        // Does a closest hexagon tile exist?
        if (closest != null && closest.GetComponent<HexagonCell>().Player == player && Mathf.Sqrt(minimalDistance) < 2 * hexagonGrid.HexCellOuterRadius)
        {
            // Set ingredient position to the middle of the hexcell
            ingredient.transform.position = new Vector3(closest.position.x, closest.position.y, ingredient.transform.position.z);

            // Setup countdown for ingredient
            var countdown = Instantiate(countdownPrefab, ingredient.transform);
            countdown.RefreshCountdown(ingredientCountdownTime, ingredient, closest.gameObject.GetComponent<HexagonCell>());
            return true;
        }
        return false;
    }

    public void StartGame(Players[] players)
    {
        GlobalData.ClearScores();

        foreach(var player in players)
        {
          GlobalData.AddToScore(player, 0);
        }

        GlobalData.SetPlayerScoreView(PlayerScoreView);
        
        this.players = players;
        ShuffleCannonWagonStartPositions();

        var nplayers = 0;
        //Spawn unicorn and train for each player
        foreach(var player in GlobalData.Players)
        {
            var randomPosition = hexagonGrid.transform.GetChild(Random.Range(0, hexagonGrid.transform.childCount)).position;
            var unicorn = Instantiate(unicornPrefab, randomPosition, Quaternion.identity);
            unicorn.player = player;

            var trainTransform = CannonWaggonStartLocations.GetChild(nplayers);
            var train = Instantiate(TrainPrefab,trainTransform.position,trainTransform.rotation);
            train.GetComponentInChildren<CannonWaggon>().player = player;
      
            train.GetComponentInChildren<TrainColor>().SetColor(Constants.PlayerColors[player]);

            nplayers++;
        }
    }

    private void ShuffleCannonWagonStartPositions()
    {
        Assert.IsTrue(CannonWaggonStartLocations.childCount >= players.Length);

        int n = CannonWaggonStartLocations.childCount;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(Random.value * (n - i));
            Transform t = CannonWaggonStartLocations.GetChild(r);
            CannonWaggonStartLocations.GetChild(i).SetSiblingIndex(r);
            t.SetSiblingIndex(i);
        }
  }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape was pressed! Quitting game!");
            Application.Quit();
        }
    }
}
