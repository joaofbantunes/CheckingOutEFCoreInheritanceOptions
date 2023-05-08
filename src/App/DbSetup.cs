namespace App;

public static class DbSetup
{
    public static async Task InitializeAsync(Func<InheritanceDbContext> factory)
    {
        var (database, isSeeded) = await CreateIfNotExistsAsync(factory);

        if (isSeeded)
        {
            Print(database, "seed already done");
            return;
        }

        Print(database, "seeding...");

        await SeedAsync(factory);

        Print(database, "done!");
    }

    private static async Task<(string database, bool isSeeded)> CreateIfNotExistsAsync(Func<InheritanceDbContext> factory)
    {
        await using var db = factory();

        Print(db.Designation, "initializing...");

        _ = await db.Database.EnsureCreatedAsync();

        return (db.Designation, db.TphEntities.Any());
    }

    private static async Task SeedAsync(Func<InheritanceDbContext> factory)
    {
        var totalSeeded = 0;

        await Parallel.ForEachAsync(
            Enumerable.Range(1, 100_000),
            new ParallelOptions
            {
                MaxDegreeOfParallelism = 50 // kinda random, but whatever =p
            },
            async (i, _) =>
            {
                long pretendForeignKey = i / 100 + 1;

                await using var db = factory();

                db
                    .TphEntities
                    .AddRange(CreateTphEntities(i, pretendForeignKey));

                db
                    .TptEntities
                    .AddRange(CreateTptEntities(i, pretendForeignKey));

                db
                    .TpcEntities
                    .AddRange(CreateTpcEntities(i, pretendForeignKey));

                await db.SaveChangesAsync();

                var currentSeeded = Interlocked.Add(ref totalSeeded, 10);
                if (currentSeeded % 100_000 == 0)
                {
                    Print(db.Designation, $"{currentSeeded} seeded");
                }
            });
    }

    private static void Print(string database, string message)
        => Console.WriteLine($"{DateTime.Now} - {database} - {message}");

    private static TphBaseEntity[] CreateTphEntities(int i, long pretendForeignKey)
        => new TphBaseEntity[]
        {
            new TphEntity1
            {
                Id = i * 10,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10,
                Property2 = i * 10,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity2
            {
                Id = i * 10 + 1,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 1,
                Property2 = i * 10 + 1,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity3
            {
                Id = i * 10 + 2,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 2,
                Property2 = i * 10 + 2,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity4
            {
                Id = i * 10 + 3,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 3,
                Property2 = i * 10 + 3,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity5
            {
                Id = i * 10 + 4,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 4,
                Property2 = i * 10 + 4,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity6
            {
                Id = i * 10 + 5,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 5,
                Property2 = i * 10 + 5,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity7
            {
                Id = i * 10 + 6,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 6,
                Property2 = i * 10 + 6,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity8
            {
                Id = i * 10 + 7,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 7,
                Property2 = i * 10 + 7,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity9
            {
                Id = i * 10 + 8,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 8,
                Property2 = i * 10 + 8,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TphEntity10
            {
                Id = i * 10 + 9,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 9,
                Property2 = i * 10 + 9,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            }
        };

    private static TptBaseEntity[] CreateTptEntities(int i, long pretendForeignKey)
        => new TptBaseEntity[]
        {
            new TptEntity1
            {
                Id = i * 10,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10,
                Property2 = i * 10,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity2
            {
                Id = i * 10 + 1,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 1,
                Property2 = i * 10 + 1,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity3
            {
                Id = i * 10 + 2,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 2,
                Property2 = i * 10 + 2,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity4
            {
                Id = i * 10 + 3,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 3,
                Property2 = i * 10 + 3,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity5
            {
                Id = i * 10 + 4,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 4,
                Property2 = i * 10 + 4,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity6
            {
                Id = i * 10 + 5,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 5,
                Property2 = i * 10 + 5,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity7
            {
                Id = i * 10 + 6,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 6,
                Property2 = i * 10 + 6,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity8
            {
                Id = i * 10 + 7,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 7,
                Property2 = i * 10 + 7,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity9
            {
                Id = i * 10 + 8,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 8,
                Property2 = i * 10 + 8,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TptEntity10
            {
                Id = i * 10 + 9,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 9,
                Property2 = i * 10 + 9,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            }
        };

    private static TpcBaseEntity[] CreateTpcEntities(int i, long pretendForeignKey)
        => new TpcBaseEntity[]
        {
            new TpcEntity1
            {
                Id = i * 10,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10,
                Property2 = i * 10,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity2
            {
                Id = i * 10 + 1,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 1,
                Property2 = i * 10 + 1,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity3
            {
                Id = i * 10 + 2,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 2,
                Property2 = i * 10 + 2,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity4
            {
                Id = i * 10 + 3,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 3,
                Property2 = i * 10 + 3,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity5
            {
                Id = i * 10 + 4,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 4,
                Property2 = i * 10 + 4,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity6
            {
                Id = i * 10 + 5,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 5,
                Property2 = i * 10 + 5,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity7
            {
                Id = i * 10 + 6,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 6,
                Property2 = i * 10 + 6,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity8
            {
                Id = i * 10 + 7,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 7,
                Property2 = i * 10 + 7,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity9
            {
                Id = i * 10 + 8,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 8,
                Property2 = i * 10 + 8,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            },
            new TpcEntity10
            {
                Id = i * 10 + 9,
                PretendForeignKey = pretendForeignKey,
                AdditionalBaseProperty1 = Guid.NewGuid(),
                AdditionalBaseProperty2 = i % 2 == 0,
                AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
                Property1 = i * 10 + 9,
                Property2 = i * 10 + 9,
                Property3 = Guid.NewGuid(),
                Property4 = i % 2 == 0,
                Property5 = Guid.NewGuid().ToString(),
            }
        };
}