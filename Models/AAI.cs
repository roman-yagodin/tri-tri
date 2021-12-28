using System;
using Godot;

public abstract class AAI
{
    public abstract PlayCardThinkResult ThinkOnPlayCard (ABoard board, ADeal deal);
}
