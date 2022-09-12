namespace TriTri;

public class PlayCardEventArgs
{
    public PlayCardThinkResult PlayCardThinkResult { get; set; }
}

public class RotateCardEventArgs
{
    public RotateDirection RotateDirection { get; set; }
}