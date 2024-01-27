using System.Collections;
using UnityEngine.Playables;

public static class TimelineExtensions
{
    public static IEnumerator PlayCoroutine(this PlayableDirector director)
    {
        director.Play();
        
        while (director.state == PlayState.Playing)
            yield return null;
    }
}
