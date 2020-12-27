using System;
using Godot;

public struct CardResult
{
    public int CardIndex;

    public BoardCoords BoardCoords;
}

public interface IAI
{
    CardResult ThinkOn (IBoard board, IDeal deal);
}

public class RandomAI : IAI
{
    public CardResult ThinkOn (IBoard board, IDeal deal)
    {
        return new CardResult {
            CardIndex = deal.TryGetRandomCardIndex (),
            BoardCoords = board.TryGetRandomEmptyTile ()
        };
    }
}