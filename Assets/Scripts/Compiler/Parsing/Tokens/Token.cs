using System.Collections.Generic;
using UnityEngine;

public abstract class Token : MonoBehaviour
{
    public abstract int VariablesRequired();
    public abstract int ValuesRequired();
    public abstract Scope Parse(Scope currentScope, ref Queue<Token> tokens);
}