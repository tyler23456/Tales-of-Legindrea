using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWriter : MonoBehaviour, ITypeWriter
{
    public bool isActive { get; set; } = false;
    public bool isDoneScrolling => body.maxVisibleCharacters >= body.text.Length;
    public Action onBegin { get; set; } = () => { };
    public Action onScroll { get; set; } = () => { };
    public Action onPaused { get; set; } = () => { };
    public Action onEnd { get; set; } = () => { };
    public Func<bool> onEndPredicate { get; set; } = () => true;

    TMP_Text body;

    HashSet<char> pauseCharacters = new HashSet<char> { '!', '?', '.' };
    char getChar => body.text[body.maxVisibleCharacters - 1];

    public void ResetEvents()
    {
        onBegin = () => { };
        onScroll = () => { };
        onPaused = () => { };
        onEnd = () => { };
    }

    public void SetText(TMP_Text text)
    {
        this.body = text;
        text.maxVisibleCharacters = 1;
    }

    public void Write()
    {
        this.StartCoroutine(Routine());       
    }

    IEnumerator Routine()
    {
        isActive = true;
        onBegin.Invoke();

        while (isActive && body.text.Length > 0)
        {   
            char c = getChar;

            if (pauseCharacters.Contains(c))
            {
                onPaused.Invoke();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || !isActive || body.text.Length == 0 || !pauseCharacters.Contains(getChar));
            }

            if (isDoneScrolling && onEndPredicate.Invoke())
                break;

            body.maxVisibleCharacters = Mathf.Clamp(body.maxVisibleCharacters + 1, 1, body.text.Length);

            onScroll.Invoke();
            yield return new WaitForSecondsRealtime(0.01f);
        }

        onEnd.Invoke();
        isActive = false;
    }
}
