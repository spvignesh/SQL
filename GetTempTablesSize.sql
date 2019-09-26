USE tempdb;
WITH KB
AS (SELECT t.name,
           SUM(a.used_pages) * 8 AS KBUsed,
           p.rows AS [Rows]
    FROM sys.tables t
        INNER JOIN sys.partitions p
            ON t.object_id = p.object_id
        INNER JOIN sys.allocation_units a
            ON a.container_id = p.partition_id
    WHERE t.is_ms_shipped = 0
    GROUP BY t.name,
             p.rows
   ) SELECT * FROM KB
   
   --Table Variables are stored in temp table with #AXXXXXX etc. To free up the space occupied by table variables restart the db server engine via ssms.
