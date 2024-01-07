using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public interface ITypeWriter
{
    bool isActive { get; set; }
    bool isDoneScrolling { get; }
    Action onBegin { get; set; }
    Action onScroll { get; set; }
    Action onPaused { get; set; }
    Action onEnd { get; set; }
    Func<bool> onEndPredicate { get; set; }
    void ResetEvents();
    void SetText(TMP_Text text);
    void Write();
}
