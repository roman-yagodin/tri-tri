using System;
using Godot;

public struct PlayCardThinkResult
{
    public int CardIndex;

    public BoardCoords BoardCoords;
}

public interface IAI
{
    PlayCardThinkResult ThinkOn (IBoard board, IDeal deal);
}

public class RandomAI : IAI
{
    public PlayCardThinkResult ThinkOn (IBoard board, IDeal deal)
    {
        return new PlayCardThinkResult {
            CardIndex = deal.TryGetRandomCardIndex (),
            BoardCoords = board.TryGetRandomEmptyTile ()
        };
    }
}