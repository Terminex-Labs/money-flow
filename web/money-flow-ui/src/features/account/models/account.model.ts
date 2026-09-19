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
    typeAccountName: string,
    currency: AccountDataCurrencyResponse,
    balance: number,
    isActive: boolean,
}

export interface AccountDataCurrencyResponse {
    id: string,
    shortName: string,
    unicode: string,
    fullName: string
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