namespace App;

public abstract class TpcBaseEntity
{
    public required int Id { get; init; }

    public required long PretendForeignKey { get; init; }

    public required Guid AdditionalBaseProperty1 { get; init; }

    public required bool AdditionalBaseProperty2 { get; init; }

    public required string AdditionalBaseProperty3 { get; init; }
}

public sealed class TpcEntity1 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity2 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity3 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity4 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity5 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity6 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity7 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity8 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity9 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TpcEntity10 : TpcBaseEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}