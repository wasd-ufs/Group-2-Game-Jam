public class ProgramFiller : TokenFiller
{
    public int slot = 0;


    public override void FillVariable(ref Variable variable)
    {
        variable = null;
    }

    public override void FillValue(ref IValue value)
    {
        value = null;
    }

    public override void FillProgram(ref AbstractSyntaxTreeNode program)
    {
        program = ProgramSlots.PlayerSlots[slot];
    }

    public override void FillScope(ref Scope scope)
    {
        scope = new Scope();
    }
}