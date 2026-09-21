SELECT 
    a.id as "Id",
    a.name as "Name",
    a.type_account_id as "TypeAccountId",
    a.currency_id as "CurrencyId",
    a.balance as "Balance",
    a.is_active as "IsActive"
FROM accounts a 
WHERE a.user_id = @userId 
AND a.id = @accountId