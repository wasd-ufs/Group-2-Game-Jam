using UnityEngine;

public abstract class TokenFiller : MonoBehaviour
{
    public abstract void FillVariable(ref Variable variable);
    public abstract void FillValue(ref IValue value);
    public abstract void FillProgram(ref AbstractSyntaxTreeNode program);
    public abstract void FillScope(ref Scope scope);
}