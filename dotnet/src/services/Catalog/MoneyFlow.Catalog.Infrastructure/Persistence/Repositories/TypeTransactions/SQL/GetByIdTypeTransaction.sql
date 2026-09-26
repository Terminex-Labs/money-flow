SELECT 
    t.id as "Id",
    t.name as "Name"
FROM type_transactions t 
WHERE t.id = @typeTransactionId