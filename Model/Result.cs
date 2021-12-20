namespace Model;

public class Result<TSuccess, TFailure>
{
    private readonly TFailure _failure;
    private readonly bool _succeeded;
    private readonly TSuccess _success;

    private Result(TSuccess success)
    {
        _success = success;
        _succeeded = true;
    }

    private Result(TFailure failure)
    {
        _failure = failure;
        _succeeded = false;
    }

    public bool Succeeded => _succeeded;
    public TFailure Failure => !_succeeded ? _failure : throw new NotSupportedException();
    public TSuccess Success => _succeeded ? _success : throw new NotSupportedException();

    public static Result<TSuccess, TFailure> Failed(TFailure failure)
    {
        return new Result<TSuccess, TFailure>(failure);
    }

    public static Result<TSuccess, TFailure> Create(TSuccess success)
    {
        return new Result<TSuccess, TFailure>(success);
    }
}

public class Result<TFailure>
{
    private readonly TFailure _failure;
    private readonly bool _succeeded;

    private Result()
    {
        _succeeded = true;
    }

    private Result(TFailure failure)
    {
        _failure = failure;
        _succeeded = false;
    }

    public bool Succeeded => _succeeded;
    public TFailure Failure => !_succeeded ? _failure : throw new NotSupportedException();

    public static Result<TFailure> Failed(TFailure failure)
    {
        return new Result<TFailure>(failure);
    }

    public static Result<TFailure> Create()
    {
        return new Result<TFailure>();
    }
}