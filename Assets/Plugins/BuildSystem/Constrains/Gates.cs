public interface IGate
{
    public bool Operate(bool b1, bool b2);
}


public enum Gates
{
    And,
    Or
}


public class And : IGate
{
    public bool Operate(bool b1, bool b2) => b1 && b2;
}

public class Or : IGate
{
    public bool Operate(bool b1, bool b2) => b1 || b2;
}
