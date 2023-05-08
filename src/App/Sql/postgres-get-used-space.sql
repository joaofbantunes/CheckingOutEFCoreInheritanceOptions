
SELECT pg_size_pretty(pg_total_relation_size('"public"."TphEntities"'));

SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities1"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities2"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities3"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities4"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities5"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities6"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities7"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities8"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities9"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TptEntities10"'));

SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities1"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities2"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities3"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities4"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities5"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities6"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities7"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities8"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities9"'));
SELECT pg_size_pretty(pg_total_relation_size('"public"."TpcEntities10"'));

-- https://stackoverflow.com/questions/41991380/whats-the-difference-between-pg-table-size-pg-relation-size-pg-total-relatio
-- pg_table_size -> just table
-- pg_total_relation_size -> table + indexes

SELECT implementation_type,
       ROUND(SUM(sizes.table_size) / 1024 / 1024) table_size_mb,
       ROUND(SUM(sizes.index_size) / 1024 / 1024) index_size_mb
FROM (SELECT table_name,
        substring(table_name FOR 3) implementation_type,
        pg_table_size('"public"."' || table_name || '"') table_size,
        pg_total_relation_size('"public"."' || table_name || '"') - pg_table_size('"public"."' || table_name || '"') index_size
FROM information_schema.tables
WHERE table_schema = 'public') as sizes
GROUP BY implementation_type;