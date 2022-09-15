using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardEventManager
{

    public static event Action<Level> OnLevelLoaded = delegate (Level level) { };

    public static event Action OnBoardUpdated = delegate () { };

    public static event Action<bool> OnInputLocked = delegate (bool b) { };

    public static event Action OnObjectiveAchieved = delegate () { };
    public static event Action OnWin = delegate () { };
    public static event Action OnDefeat = delegate () { };


    public static event Action<Vector2Int, ABoardElement> OnTileCreated = delegate (Vector2Int pos, ABoardElement tile) { };
    public static event Action<Vector2Int, bool, ABoardElement[,]> OnTileDestroyed = delegate (Vector2Int pos, bool broken, ABoardElement[,] board) { };
    public static event Action<Vector2Int, ABoardElement[,]> OnTileMatched = delegate (Vector2Int pos, ABoardElement[,] board) { };
    public static event Action<Vector2Int, Vector2Int> OnTileMove = delegate (Vector2Int pos, Vector2Int target) { };
    public static event Action<Vector2Int, Vector2Int> OnTileSwap = delegate (Vector2Int pos, Vector2Int target) { };
    public static event Action<int, ObjectiveType> OnObjectiveUpdated = delegate (int amount, ObjectiveType type) { };

    public static event Action<Booster, Vector2Int> OnBoosterGenerated = delegate (Booster booster, Vector2Int pos) { };
    public static event Action<Vector2Int> OnTileDissapearWithNoEffect = delegate (Vector2Int pos) { };

    public static event Action<ActiveBooster> OnActiveBoosterSet = delegate (ActiveBooster booster) { };
    public static event Action<ActiveBooster> OnActiveBoosterUsed = delegate (ActiveBooster booster) { };

    public static event Action OnMoveDone = delegate () { };



    public static void NotifyOnLevelLoaded(Level level) => OnLevelLoaded(level);

    public static void NotifyOnTileDestroyed(Vector2Int position, bool broken, ABoardElement[,] board) => OnTileDestroyed(position, broken, board);
    public static void NotifyOnTileMatched(Vector2Int position, ABoardElement[,] board) => OnTileMatched(position, board);
    public static void NotifyOnTileCreated(Vector2Int position, ABoardElement tile) => OnTileCreated(position, tile);


    public static void NotifyOnBoardUpdated() => OnBoardUpdated();
    

    public static void NotifyOnTileMove(Vector2Int pos, Vector2Int target) => OnTileMove(pos, target);
    public static void NotifyOnTileSwap(Vector2Int pos, Vector2Int target) => OnTileSwap(pos, target);


    public static void NotifyOnObjectiveUpdated(int amount, ObjectiveType type) => OnObjectiveUpdated(amount, type);
    

    public static void NotifyOnObjectiveAchieved() => OnObjectiveAchieved();

    public static void NotifyOnWin() => OnWin();  

    public static void NotifyOnDefeat() => OnDefeat();

    public static void NotifyOnInputLocked(bool isLocked) => OnInputLocked(isLocked);


    public static void NotifyOnBoosterGenerated(Booster booster, Vector2Int pos) => OnBoosterGenerated(booster, pos);
    public static void NotifyOnTileDissapearWithNoEffect(Vector2Int pos) => OnTileDissapearWithNoEffect(pos);
    
    public static void NotifyOnActiveBoosterSet(ActiveBooster booster) => OnActiveBoosterSet(booster);

    public static void NotifyOnActiveBoosterUsed(ActiveBooster booster) => OnActiveBoosterUsed(booster);

    public static void NotifyOnMoveDone() => OnMoveDone();

}
