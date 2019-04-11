--Group by specific columns and select all the columns.

select t1.*
from [biz].[Table] t1
join
(
    SELECT max(PK) as PK
    FROM [biz].[Table] 
    GROUP BY Name,City
) t2 on t1.PK = t2.PK
