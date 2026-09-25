SELECT 
    t.id as "Id",
    t.name as "Name"
FROM type_accounts t 
WHERE t.id = @typeAccountId