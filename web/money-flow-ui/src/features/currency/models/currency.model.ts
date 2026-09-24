export interface CurrencyView {
    id: string,
    shortName: string,
    unicode: string,
    fullName: string,
}

export interface CreateCurrencyRequest {
    shortName: string,
    unicode: string,
    fullName: string,
}

export interface UpdateCurrencyRequest {
    id: string,
    shortName: string,
    unicode: string,
    fullName: string,
}

export interface CreatedCurrencyResponse {
    id: string
}

export interface CurrencyResponse {
    id: string,
    shortName: string,
    unicode: string,
    fullName: string,
}