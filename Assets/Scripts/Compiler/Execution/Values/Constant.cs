public class Constant : IValue
{
    public int value;
 
    public Constant(int value) => this.value = value;
    public int Get()
    {
        return value;
    }
}