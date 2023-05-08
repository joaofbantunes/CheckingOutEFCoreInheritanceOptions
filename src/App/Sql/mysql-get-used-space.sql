SELECT implementation_type,
       ROUND(SUM(sizes.table_size) / 1024 / 1024) table_size_mb,
       ROUND(SUM(sizes.index_size) / 1024 / 1024) index_size_mb
FROM (SELECT
  TABLE_NAME AS table_name,
  SUBSTRING(TABLE_NAME FROM 1 FOR 3) AS implementation_type,
  DATA_LENGTH AS table_size,
  INDEX_LENGTH AS index_size
FROM
  information_schema.TABLES
WHERE
  TABLE_SCHEMA = "CheckingOutEFCoreInheritanceOptions") as sizes
GROUP BY implementation_type;