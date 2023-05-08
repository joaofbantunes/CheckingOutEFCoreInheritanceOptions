
sp_spaceused 'TphEntities';

sp_spaceused 'TptEntities';
sp_spaceused 'TptEntities1';
sp_spaceused 'TptEntities2';
sp_spaceused 'TptEntities3';
sp_spaceused 'TptEntities4';
sp_spaceused 'TptEntities5';
sp_spaceused 'TptEntities6';
sp_spaceused 'TptEntities7';
sp_spaceused 'TptEntities8';
sp_spaceused 'TptEntities9';
sp_spaceused 'TptEntities10';

sp_spaceused 'TpcEntities1';
sp_spaceused 'TpcEntities2';
sp_spaceused 'TpcEntities3';
sp_spaceused 'TpcEntities4';
sp_spaceused 'TpcEntities5';
sp_spaceused 'TpcEntities6';
sp_spaceused 'TpcEntities7';
sp_spaceused 'TpcEntities8';
sp_spaceused 'TpcEntities9';
sp_spaceused 'TpcEntities10';

;with cte as (
    SELECT
        t.name as table_name,
        SUM (s.used_page_count) as used_pages_count,
        SUM (CASE
                 WHEN (i.index_id < 2) THEN (in_row_data_page_count + lob_used_page_count + row_overflow_used_page_count)
                 ELSE lob_used_page_count + row_overflow_used_page_count
            END) as pages
    FROM sys.dm_db_partition_stats  AS s
             JOIN sys.tables AS t ON s.object_id = t.object_id
             JOIN sys.indexes AS i ON i.[object_id] = t.[object_id] AND s.index_id = i.index_id
    GROUP BY t.name
)
    ,cte2 as(select
                 cte.table_name,
                 substring(cte.table_name, 1, 3) as implementation_type,
                 (cte.pages * 8.) as TableSizeInKB,
                 ((CASE WHEN cte.used_pages_count > cte.pages
                            THEN cte.used_pages_count - cte.pages
                        ELSE 0
                     END) * 8.) as IndexSizeInKB
             from cte
    )
 select implementation_type,
        ROUND(SUM(TableSizeInKB / 1024), 0) as table_size_mb,
        ROUND(SUM(IndexSizeInKB / 1024), 0) as index_size_mb
 from cte2
 GROUP BY implementation_type;