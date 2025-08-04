namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Result Pattern para tratamento de erros sem exceptions.
/// Inspirado no Result do F# e Rust.
/// </summary>
public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; protected set; } = string.Empty;
    public List<string> Errors { get; protected set; } = new();

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("Resultado de sucesso não pode ter erro");

        if (!isSuccess && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("Resultado de falha deve ter erro");

        IsSuccess = isSuccess;
        Error = error;
    }

    protected Result(bool isSuccess, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors.ToList();
        Error = string.Join("; ", errors);
    }

    /// <summary>
    /// Cria resultado de sucesso
    /// </summary>
    public static Result Success() => new(true, string.Empty);

    /// <summary>
    /// Cria resultado de falha
    /// </summary>
    public static Result Failure(string error) => new(false, error);

    /// <summary>
    /// Cria resultado de falha com múltiplos erros
    /// </summary>
    public static Result Failure(IEnumerable<string> errors) => new(false, errors);
}

/// <summary>
/// Result que carrega um valor em caso de sucesso
/// </summary>
/// <typeparam name="T">Tipo do valor</typeparam>
public class Result<T> : Result
{
    public T? Value { get; private set; }

    protected Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    protected Result(T value, bool isSuccess, IEnumerable<string> errors) : base(isSuccess, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Cria resultado de sucesso com valor
    /// </summary>
    public static Result<T> Success(T value) => new(value, true, string.Empty);

    /// <summary>
    /// Cria resultado de falha
    /// </summary>
    public static new Result<T> Failure(string error) => new(default(T)!, false, error);

    /// <summary>
    /// Cria resultado de falha com múltiplos erros
    /// </summary>
    public static new Result<T> Failure(IEnumerable<string> errors) => new(default(T)!, false, errors);

    /// <summary>
    /// Converte implicitamente o valor para Result<T>
    /// </summary>
    public static implicit operator Result<T>(T value) => Success(value);
}