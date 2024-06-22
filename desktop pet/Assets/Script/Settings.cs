using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATOR PARAMETERS
    public static int idle = Animator.StringToHash("Idle");
    public static int patrol = Animator.StringToHash("Patrol");
    public static int stagger = Animator.StringToHash("Stagger");
    public static int drag = Animator.StringToHash("Drag");
    public static int fall = Animator.StringToHash("Fall");
    public static int speech = Animator.StringToHash("Speech");
    public static int sleep = Animator.StringToHash("Sleep");
    public static int attackStart = Animator.StringToHash("AttackStart");
    public static int attackActive = Animator.StringToHash("AttackActive");
    public static int stunStart = Animator.StringToHash("StunStart");
    public static int stunActive = Animator.StringToHash("StunActive");
    #endregion
}
