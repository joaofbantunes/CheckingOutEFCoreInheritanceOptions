using Microsoft.EntityFrameworkCore;

namespace App;

// the interface is just so I can save some copy pasting when setting up the database
public interface ITphLeafEntity
{
    public int Property1 { get; init; }

    public long Property2 { get; init; }

    public Guid Property3 { get; init; }

    public bool Property4 { get; init; }

    public string Property5 { get; init; }
}

public abstract class TphBaseEntity
{
    public required int Id { get; init; }

    public required long PretendForeignKey { get; init; }

    public required Guid AdditionalBaseProperty1 { get; init; }

    public required bool AdditionalBaseProperty2 { get; init; }

    public required string AdditionalBaseProperty3 { get; init; }
}

public sealed class TphEntity1 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity2 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity3 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity4 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity5 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity6 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity7 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity8 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity9 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}

public sealed class TphEntity10 : TphBaseEntity, ITphLeafEntity
{
    public required int Property1 { get; init; }

    public required long Property2 { get; init; }

    public required Guid Property3 { get; init; }

    public required bool Property4 { get; init; }

    public required string Property5 { get; init; }
}