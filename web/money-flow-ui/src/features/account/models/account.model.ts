export interface AccountView {
    id: string | null,
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

export interface UpdateAccountRequest {
    id: string,
    name: string,
    typeAccountId: string,
    currencyId: string,
    balance: number,
    isActive: boolean
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