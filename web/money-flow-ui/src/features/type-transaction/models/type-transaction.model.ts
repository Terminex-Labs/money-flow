export interface TypeTransactionView {
    id: string,
    name: string
}

export interface CreateTypeTransactionRequest {
    name: string
}

export interface UpdateTypeTransactionRequest {
    id: string,
    name: string
}

export interface CreatedTypeTransactionResponse {
    id: string
}

export interface TypeTransactionResponse {
    id: string,
    name: string
}