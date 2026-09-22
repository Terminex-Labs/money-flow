export interface Account {
    name: string,
    typeAccountId: string | null,
    currencyId: string | null,
    balance: number,
    isActive: boolean
}

export interface CreateAccountRequest {
    name: string,
    typeAccountId: string,
    currencyId: string,
    balance: number,
    isActive: boolean
}

export interface UpdateAccountNameRequest {
    id: string,
    name: string
}

export interface CreatedAccountResponse {
    id: string
}

export interface AccountResponse {
    id: string,
    name: string,
    typeAccount: 
    {
        id: string,
        name: string
    },
    currency: 
    {
        id: string,
        shortName: string,
        unicode: string,
        fullName: string
    },
    balance: number,
    isActive: boolean,
}

export interface CurrencyResponse {
    id: string,
    shortName: string,
    unicode: string,
    fullName: string
}

export interface TypeAccountResponse {
    id: string,
    name: string
}