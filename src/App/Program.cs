using System.Linq.Expressions;
using App;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

await Task.WhenAll(
    DbSetup.InitializeAsync(() => new PostgresInheritanceDbContext()),
    DbSetup.InitializeAsync(() => new MySqlInheritanceDbContext()),
    DbSetup.InitializeAsync(() => new SqlServerInheritanceDbContext()),
    DbSetup.InitializeAsync(() => new SqlServerSparseInheritanceDbContext()));

InheritanceBenchmark.DoSanityChecks();

BenchmarkRunner.Run<InheritanceBenchmark>();

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams, BenchmarkLogicalGroupRule.ByCategory)]
[RankColumn, MinColumn, MaxColumn, CategoriesColumn]
public class InheritanceBenchmark
{
    private const int StartInsertId = 2_000_000;

    private int _lastInsertedId;

    [Params("postgres", "mysql", "sqlserver", "sqlserversparse")]
    public string Database { get; set; } = null!;

    private InheritanceDbContext CreateContext()
        => Database switch
        {
            "postgres" => new PostgresInheritanceDbContext(),
            "mysql" => new MySqlInheritanceDbContext(),
            "sqlserver" => new SqlServerInheritanceDbContext(),
            "sqlserversparse" => new SqlServerSparseInheritanceDbContext()
        };

    [GlobalSetup]
    public void Setup()
    {
        // just in case we abort benchmarks before running cleanup, cleaning the data before
        CleanUpDatabaseForBenchmarks();
    }

    [GlobalCleanup]
    public void CleanUp()
    {
        CleanUpDatabaseForBenchmarks();
    }


    [Benchmark(Baseline = true)]
    [BenchmarkCategory("OffsetPagination")]
    public List<TphBaseEntity> TphOffsetPagination()
    {
        using var context = CreateContext();
        return QueryOffsetPagination(context.TphEntities, e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("OffsetPagination")]
    public List<TptBaseEntity> TptOffsetPagination()
    {
        using var context = CreateContext();
        return QueryOffsetPagination(context.TptEntities, e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("OffsetPagination")]
    public List<TpcBaseEntity> TpcOffsetPagination()
    {
        using var context = CreateContext();
        return QueryOffsetPagination(context.TpcEntities, e => e.Id);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("IndexPagination")]
    public List<TphBaseEntity> TphIndexPagination()
    {
        using var context = CreateContext();

        var minimumId = Random.Shared.Next(0, 998_999);

        return QueryIndexPagination(
            context.TphEntities,
            e => e.Id > minimumId,
            e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("IndexPagination")]
    public List<TptBaseEntity> TptIndexPagination()
    {
        using var context = CreateContext();

        var minimumId = Random.Shared.Next(0, 998_999);

        return QueryIndexPagination(
            context.TptEntities,
            e => e.Id > minimumId,
            e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("IndexPagination")]
    public List<TpcBaseEntity> TpcIndexPagination()
    {
        using var context = CreateContext();

        var minimumId = Random.Shared.Next(0, 998_999);

        return QueryIndexPagination(
            context.TpcEntities,
            e => e.Id > minimumId,
            e => e.Id);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("FilterByForeignKey")]
    public List<TphBaseEntity> TphFilterByForeignKey()
    {
        using var context = CreateContext();

        var foreignKey = Random.Shared.NextInt64(1, 1000);

        return QueryIndexPagination(
            context.TphEntities,
            e => e.PretendForeignKey == foreignKey,
            e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("FilterByForeignKey")]
    public List<TptBaseEntity> TptFilterByForeignKey()
    {
        using var context = CreateContext();

        var foreignKey = Random.Shared.NextInt64(1, 1000);

        return QueryWithFilter(
            context.TptEntities,
            e => e.PretendForeignKey == foreignKey,
            e => e.Id);
    }

    [Benchmark]
    [BenchmarkCategory("FilterByForeignKey")]
    public List<TpcBaseEntity> TpcFilterByForeignKey()
    {
        using var context = CreateContext();

        var foreignKey = Random.Shared.NextInt64(1, 1000);

        return QueryWithFilter(
            context.TpcEntities,
            e => e.PretendForeignKey == foreignKey,
            e => e.Id);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("SingleById")]
    public TphBaseEntity? TphSingleById()
    {
        using var context = CreateContext();
        return context.TphEntities.Find(Random.Shared.Next(0, 999_999));
    }

    [Benchmark]
    [BenchmarkCategory("SingleById")]
    public TptBaseEntity? TptSingleById()
    {
        using var context = CreateContext();
        return context.TptEntities.Find(Random.Shared.Next(0, 999_999));
    }

    [Benchmark]
    [BenchmarkCategory("SingleById")]
    public TpcBaseEntity? TpcSingleById()
    {
        using var context = CreateContext();
        return context.TpcEntities.Find(Random.Shared.Next(0, 999_999));
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("InsertSingle")]
    public TphBaseEntity TphInsertSingle()
    {
        using var context = CreateContext();
        var id = ++_lastInsertedId;

        var toInsert = new TphEntity8
        {
            Id = id,
            PretendForeignKey = 1,
            AdditionalBaseProperty1 = Guid.NewGuid(),
            AdditionalBaseProperty2 = id % 2 == 0,
            AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
            Property1 = id * 10,
            Property2 = id * 10,
            Property3 = Guid.NewGuid(),
            Property4 = id % 2 == 0,
            Property5 = Guid.NewGuid().ToString(),
        };

        context.SaveChanges();
        return toInsert;
    }

    [Benchmark]
    [BenchmarkCategory("InsertSingle")]
    public TptBaseEntity TptInsertSingle()
    {
        using var context = CreateContext();
        var id = ++_lastInsertedId;

        var toInsert = new TptEntity8
        {
            Id = id,
            PretendForeignKey = 1,
            AdditionalBaseProperty1 = Guid.NewGuid(),
            AdditionalBaseProperty2 = id % 2 == 0,
            AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
            Property1 = id * 10,
            Property2 = id * 10,
            Property3 = Guid.NewGuid(),
            Property4 = id % 2 == 0,
            Property5 = Guid.NewGuid().ToString(),
        };

        context.SaveChanges();
        return toInsert;
    }

    [Benchmark]
    [BenchmarkCategory("InsertSingle")]
    public TpcBaseEntity TpcInsertSingle()
    {
        using var context = CreateContext();
        var id = ++_lastInsertedId;

        var toInsert = new TpcEntity8
        {
            Id = id,
            PretendForeignKey = 1,
            AdditionalBaseProperty1 = Guid.NewGuid(),
            AdditionalBaseProperty2 = id % 2 == 0,
            AdditionalBaseProperty3 = Guid.NewGuid().ToString(),
            Property1 = id * 10,
            Property2 = id * 10,
            Property3 = Guid.NewGuid(),
            Property4 = id % 2 == 0,
            Property5 = Guid.NewGuid().ToString(),
        };

        context.SaveChanges();
        return toInsert;
    }

    private static List<TEntity> QueryOffsetPagination<TEntity, TSortKey>(
        DbSet<TEntity> source,
        Expression<Func<TEntity, TSortKey>> sortKeySelector)
        where TEntity : class
        => source
            .AsNoTracking()
            .OrderBy(sortKeySelector)
            .Skip(Random.Shared.Next(400_000, 500_000))
            .Take(1_000)
            .ToList();

    private static List<TEntity> QueryIndexPagination<TEntity, TSortKey>(
        DbSet<TEntity> source,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TSortKey>> sortKeySelector)
        where TEntity : class
        => source
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(sortKeySelector)
            .Take(1_000)
            .ToList();

    private static List<TEntity> QueryWithFilter<TEntity, TSortKey>(
        DbSet<TEntity> source,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TSortKey>> sortKeySelector)
        where TEntity : class
        => source
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(sortKeySelector)
            .ToList();


    public static void DoSanityChecks()
    {
        Console.WriteLine("Running sanity checks...");
        var benchmark = new InheritanceBenchmark();
        try
        {
            benchmark.Setup();
            AssertMethods(benchmark, "postgres");
            AssertMethods(benchmark, "mysql");
            AssertMethods(benchmark, "sqlserver");
            AssertMethods(benchmark, "sqlserversparse");
        }
        finally
        {
            benchmark.CleanUp();
        }

        Console.WriteLine("Sanity checks passed!");

        static void AssertMethods(InheritanceBenchmark benchmark, string database)
        {
            benchmark.Database = database;
            if (benchmark.TphIndexPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TphIndexPagination)} result for {benchmark.Database}");
            if (benchmark.TptIndexPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TptIndexPagination)} result for {benchmark.Database}");
            if (benchmark.TpcIndexPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TpcIndexPagination)} result for {benchmark.Database}");

            if (benchmark.TphOffsetPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TphOffsetPagination)} result for {benchmark.Database}");
            if (benchmark.TptOffsetPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TptOffsetPagination)} result for {benchmark.Database}");
            if (benchmark.TpcOffsetPagination().Count != 1000)
                throw new Exception($"Unexpected {nameof(TpcOffsetPagination)} result for {benchmark.Database}");

            if (benchmark.TphFilterByForeignKey().Count != 1000)
                throw new Exception($"Unexpected {nameof(TphFilterByForeignKey)} result for {benchmark.Database}");
            if (benchmark.TptFilterByForeignKey().Count != 1000)
                throw new Exception($"Unexpected {nameof(TptFilterByForeignKey)} result for {benchmark.Database}");
            if (benchmark.TpcFilterByForeignKey().Count != 1000)
                throw new Exception($"Unexpected {nameof(TpcFilterByForeignKey)} result for {benchmark.Database}");

            if (benchmark.TphSingleById() is null)
                throw new Exception($"Unexpected {nameof(TphSingleById)} result for {benchmark.Database}");
            if (benchmark.TptSingleById() is null)
                throw new Exception($"Unexpected {nameof(TptSingleById)} result for {benchmark.Database}");
            if (benchmark.TpcSingleById() is null)
                throw new Exception($"Unexpected {nameof(TpcSingleById)} result for {benchmark.Database}");

            using (var context = benchmark.CreateContext())
            {
                if (context.TphEntities.Count() != 1_000_000)
                    throw new Exception($"Unexpected {nameof(context.TphEntities)} count for {benchmark.Database}");
                if (context.TptEntities.Count() != 1_000_000)
                    throw new Exception($"Unexpected {nameof(context.TptEntities)} count for {benchmark.Database}");
                if (context.TpcEntities.Count() != 1_000_000)
                    throw new Exception($"Unexpected {nameof(context.TpcEntities)} count for {benchmark.Database}");
            }
        }
    }

    private static void CleanUpDatabaseForBenchmarks()
    {
        DeleteRecordsCreatedDuringPreviousBenchmarks(() => new PostgresInheritanceDbContext());
        DeleteRecordsCreatedDuringPreviousBenchmarks(() => new MySqlInheritanceDbContext());
        DeleteRecordsCreatedDuringPreviousBenchmarks(() => new SqlServerInheritanceDbContext());
        DeleteRecordsCreatedDuringPreviousBenchmarks(() => new SqlServerSparseInheritanceDbContext());

        static void DeleteRecordsCreatedDuringPreviousBenchmarks(Func<InheritanceDbContext> factory)
        {
            using var db = factory();
            db.TphEntities.Where(e => e.Id >= StartInsertId).ExecuteDelete();

            db.TptEntities.RemoveRange(db.TptEntities.Where(e => e.Id >= StartInsertId));

            db.TpcEntities1.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities2.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities3.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities4.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities5.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities6.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities7.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities8.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities9.Where(e => e.Id >= StartInsertId).ExecuteDelete();
            db.TpcEntities10.Where(e => e.Id >= StartInsertId).ExecuteDelete();

            db.SaveChanges();
        }
    }
}