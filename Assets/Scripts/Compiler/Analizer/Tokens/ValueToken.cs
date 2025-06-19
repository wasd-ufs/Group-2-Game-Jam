public class ValueToken : IToken
{
    public ValueToken(int value) => this.value = value;
    
    public int value;
}