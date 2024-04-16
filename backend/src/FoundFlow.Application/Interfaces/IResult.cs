namespace FoundFlow.Application.Interfaces;

public interface IResult
{
    public bool Succeeded { get; protected set; }
    public object Data { get; protected set; }
    public int Status { get; protected set; }
}