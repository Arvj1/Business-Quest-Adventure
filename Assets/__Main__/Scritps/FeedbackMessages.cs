using System;
using System.Collections.Generic;

public static class FeedbackMessages
{
    private static readonly List<string> appreciationMessages = new List<string>
    {
        "Great job! You got it right!",
        "Awesome! You're doing really well!",
        "That's correct! Keep up the good work!",
        "Nice work! You're learning so much!",
        "Yay! That's the right answer!",
        "You did it! That was perfect!",
        "Super! You're on the right track!",
        "Brilliant! You're getting better every time!"
    };

    private static readonly List<string> gentleCorrectionMessages = new List<string>
    {
        "Oops, not quite. Let's try again!",
        "Almost there! Give it another go.",
        "That's okay! You can try once more.",
        "Good effort! Let's see if you can find the right one.",
        "Hmm, not this time. Want to try again?",
        "You're trying hard and that matters! Let’s take another shot.",
        "That’s not the one, but you’re doing great by trying!",
        "Learning takes time — let’s try again together!"
    };

    private static readonly Random rng = new Random();

    public static string GetAppreciationMessage()
    {
        return appreciationMessages[rng.Next(appreciationMessages.Count)];
    }

    public static string GetGentleCorrectionMessage()
    {
        return gentleCorrectionMessages[rng.Next(gentleCorrectionMessages.Count)];
    }
}
