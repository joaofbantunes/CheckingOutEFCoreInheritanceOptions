using Microsoft.EntityFrameworkCore;

namespace App;

public class PostgresInheritanceDbContext : InheritanceDbContext
{
    private const string ConnectionString =
        "server=localhost;port=5432;user id=user;password=pass;database=CheckingOutEFCoreInheritanceOptions";

    public PostgresInheritanceDbContext()
        : base(options => options.UseNpgsql(ConnectionString))
    {
    }
}

public class MySqlInheritanceDbContext : InheritanceDbContext
{
    private const string ConnectionString =
        "Server=localhost;Database=CheckingOutEFCoreInheritanceOptions;Uid=user;Pwd=pass;";

    public MySqlInheritanceDbContext()
        : base(options => options.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0))))
    {
    }
}

public class SqlServerInheritanceDbContext : InheritanceDbContext
{
    private const string ConnectionString =
        "Server=localhost; Database=CheckingOutEFCoreInheritanceOptions; User Id=sa;Password=StupidPassw0rd; TrustServerCertificate=True";

    public SqlServerInheritanceDbContext()
        : base(options => options.UseSqlServer(ConnectionString))
    {
    }
}

public class SqlServerSparseInheritanceDbContext : InheritanceDbContext
{
    private const string ConnectionString =
        "Server=localhost; Database=CheckingOutEFCoreInheritanceOptionsSparse; User Id=sa;Password=StupidPassw0rd; TrustServerCertificate=True";

    public SqlServerSparseInheritanceDbContext()
        : base(options => options.UseSqlServer(ConnectionString))
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureTphLeafEntity<TphEntity1>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity2>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity3>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity4>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity5>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity6>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity7>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity8>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity9>(modelBuilder);
        ConfigureTphLeafEntity<TphEntity10>(modelBuilder);


        static void ConfigureTphLeafEntity<TphLeafEntity>(ModelBuilder modelBuilder)
            where TphLeafEntity : class, ITphLeafEntity
        {
            modelBuilder
                .Entity<TphLeafEntity>()
                .Property(e => e.Property1)
                .IsSparse();

            modelBuilder
                .Entity<TphLeafEntity>()
                .Property(e => e.Property2)
                .IsSparse();

            modelBuilder
                .Entity<TphLeafEntity>()
                .Property(e => e.Property3)
                .IsSparse();

            modelBuilder
                .Entity<TphLeafEntity>()
                .Property(e => e.Property4)
                .IsSparse();

            modelBuilder
                .Entity<TphLeafEntity>()
                .Property(e => e.Property5)
                .IsSparse();
        }
    }
}

public abstract class InheritanceDbContext : DbContext
{
    private readonly Action<DbContextOptionsBuilder> _onConfiguring;

    public InheritanceDbContext(Action<DbContextOptionsBuilder> onConfiguring)
    {
        _onConfiguring = onConfiguring;
        Designation = this.GetType().Name.Replace(nameof(InheritanceDbContext), "");
    }

    public string Designation { get; }

    public DbSet<TphBaseEntity> TphEntities => Set<TphBaseEntity>();
    public DbSet<TphEntity1> TphEntities1 => Set<TphEntity1>();
    public DbSet<TphEntity2> TphEntities2 => Set<TphEntity2>();
    public DbSet<TphEntity3> TphEntities3 => Set<TphEntity3>();
    public DbSet<TphEntity4> TphEntities4 => Set<TphEntity4>();
    public DbSet<TphEntity5> TphEntities5 => Set<TphEntity5>();
    public DbSet<TphEntity6> TphEntities6 => Set<TphEntity6>();
    public DbSet<TphEntity7> TphEntities7 => Set<TphEntity7>();
    public DbSet<TphEntity8> TphEntities8 => Set<TphEntity8>();
    public DbSet<TphEntity9> TphEntities9 => Set<TphEntity9>();
    public DbSet<TphEntity10> TphEntities10 => Set<TphEntity10>();

    public DbSet<TptBaseEntity> TptEntities => Set<TptBaseEntity>();
    public DbSet<TptEntity1> TptEntities1 => Set<TptEntity1>();
    public DbSet<TptEntity2> TptEntities2 => Set<TptEntity2>();
    public DbSet<TptEntity3> TptEntities3 => Set<TptEntity3>();
    public DbSet<TptEntity4> TptEntities4 => Set<TptEntity4>();
    public DbSet<TptEntity5> TptEntities5 => Set<TptEntity5>();
    public DbSet<TptEntity6> TptEntities6 => Set<TptEntity6>();
    public DbSet<TptEntity7> TptEntities7 => Set<TptEntity7>();
    public DbSet<TptEntity8> TptEntities8 => Set<TptEntity8>();
    public DbSet<TptEntity9> TptEntities9 => Set<TptEntity9>();
    public DbSet<TptEntity10> TptEntities10 => Set<TptEntity10>();

    public DbSet<TpcBaseEntity> TpcEntities => Set<TpcBaseEntity>();
    public DbSet<TpcEntity1> TpcEntities1 => Set<TpcEntity1>();
    public DbSet<TpcEntity2> TpcEntities2 => Set<TpcEntity2>();
    public DbSet<TpcEntity3> TpcEntities3 => Set<TpcEntity3>();
    public DbSet<TpcEntity4> TpcEntities4 => Set<TpcEntity4>();
    public DbSet<TpcEntity5> TpcEntities5 => Set<TpcEntity5>();
    public DbSet<TpcEntity6> TpcEntities6 => Set<TpcEntity6>();
    public DbSet<TpcEntity7> TpcEntities7 => Set<TpcEntity7>();
    public DbSet<TpcEntity8> TpcEntities8 => Set<TpcEntity8>();
    public DbSet<TpcEntity9> TpcEntities9 => Set<TpcEntity9>();
    public DbSet<TpcEntity10> TpcEntities10 => Set<TpcEntity10>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _onConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH

        modelBuilder
            .Entity<TphBaseEntity>()
            .UseTphMappingStrategy()
            .ToTable("TphEntities")
            .HasKey(e => e.Id);

        modelBuilder
            .Entity<TphBaseEntity>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder
            .Entity<TphBaseEntity>()
            .HasIndex(e => e.PretendForeignKey);

        // TPT

        modelBuilder
            .Entity<TptBaseEntity>()
            .UseTptMappingStrategy()
            .ToTable("TptEntities")
            .HasKey(e => e.Id);

        modelBuilder
            .Entity<TptBaseEntity>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder
            .Entity<TptBaseEntity>()
            .HasIndex(e => e.PretendForeignKey);

        // TPC

        modelBuilder
            .Entity<TpcBaseEntity>()
            .UseTpcMappingStrategy()
            .HasKey(e => e.Id);

        modelBuilder
            .Entity<TpcBaseEntity>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder
            .Entity<TpcBaseEntity>()
            .HasIndex(e => e.PretendForeignKey);
    }
}