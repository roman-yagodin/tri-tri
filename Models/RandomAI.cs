namespace TriTri;

public class RandomAI : AAI
{
    public override PlayCardThinkResult ThinkOnPlayCard (ABoard board, ADeal deal)
    {
        return new PlayCardThinkResult {
            CardIndex = deal.TryGetRandomCardIndex (),
            BoardCoords = board.TryGetRandomEmptyTile ()
        };
    }
}