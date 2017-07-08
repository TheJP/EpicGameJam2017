using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Random = UnityEngine.Random;


public class Controller : MonoBehaviour
{
    [Tooltip("Play field that is managed by this controller")]
    public HexagonGrid hexagonGrid;

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

    [Tooltip("The UI text the winner is announced in")]
    public Text WinningView;

    private Players[] players;
    private List<Unicorn> unicorns = new List<Unicorn>();

    public bool DropIngredientOnPizza(Players player, Ingredient ingredient)
    {
        if (IsGameFinished())
        {
            return false;
        }

        // Check for nearby hexagon tiles
        var closest = FindClosestHexagonCell(player, ingredient.transform.position);

        // Does a closest hexagon tile exist?
        if (closest != null)
        {
            // Set ingredient position to the middle of the hexcell
            ingredient.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y, ingredient.transform.position.z);

            // Setup countdown for ingredient
            var countdown = Instantiate(countdownPrefab, ingredient.transform);
            countdown.RefreshCountdown(countdown.countdownStart, ingredient, closest);
            return true;
        }

        return false;
    }

    public void StartGame()
    {
        unicorns.Clear();
        players = GlobalData.Players.ToArray();

        WinningView.text = "";
        GlobalData.ClearScores();

        foreach (var player in players)
        {
            GlobalData.AddToScore(player, 0);
        }

        GlobalData.SetPlayerScoreView(PlayerScoreView);

        ShuffleCannonWagonStartPositions();

        var nplayers = 0;
        //Spawn unicorn and train for each player
        foreach (var player in players)
        {
            var randomPosition = hexagonGrid.transform.GetChild(Random.Range(0, hexagonGrid.transform.childCount)).position;
            randomPosition.z = 0;
            var unicorn = Instantiate(unicornPrefab, randomPosition, Quaternion.identity);
            unicorn.player = player;
            unicorns.Add(unicorn);

            var trainTransform = CannonWaggonStartLocations.GetChild(nplayers);
            var train = Instantiate(TrainPrefab, trainTransform.position, trainTransform.rotation);
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

    public void Start()
    {
        StartGame();
    }

    public void Update()
    {
        // End the game
        if (Input.GetButtonDown("BackToMenu"))
        {
            StartCoroutine(ReturnToMainMenu("Returning to the menu in {0}...", Color.white));
        }

        if (IsGameFinished())
        {
            return;
        }

        foreach (var player in players)
        {
            if (GlobalData.GetScore(player) >= GlobalData.PointsToWin)
            {
                var playerColor = Constants.PlayerColors[player];
                StartCoroutine(ReturnToMainMenu("Player " + player + " is the most loved Unicorn!", playerColor));
                break;
            }
        }
    }

    public void FixedUpdate()
    {
        foreach(var unicorn in unicorns)
        {
            var hexagonCell = FindClosestHexagonCell(null, unicorn.transform.position);

            if(hexagonCell != null && hexagonCell.IsCheesed)
            {
                unicorn.SetCheesed();
            }
        }
    }

    private IEnumerator ReturnToMainMenu(string message, Color? color)
    {
        if(color.HasValue)
        {
            WinningView.color = color.Value;
        }

        for(var i = 0; i < 5; ++i)
        {
            WinningView.text = String.Format(message, 5 - i);
            yield return new WaitForSeconds(1.0f);
        }

        SceneManager.LoadScene("MenuScene");
    }

    private bool IsGameFinished()
    {
        return WinningView.text.Length > 0;
    }

    private HexagonCell FindClosestHexagonCell(Players? player, Vector3 position)
    {
        var minimalDistance = float.PositiveInfinity;
        HexagonCell closest = null;
        for(int i = hexagonGrid.transform.childCount - 1; i >= 0; --i)
        {
            var child = hexagonGrid.transform.GetChild(i).gameObject.GetComponent<HexagonCell>();
            var distance = Vector2.SqrMagnitude(child.transform.position - position);
            if(distance < minimalDistance && (!player.HasValue || child.Player.HasValue && child.Player == player))
            {
                minimalDistance = distance;
                closest = child;
            }
        }

        if(closest != null && Mathf.Sqrt(minimalDistance) < 2 * hexagonGrid.HexCellOuterRadius)
        {
            return closest;
        }

        return null;
    }
}
